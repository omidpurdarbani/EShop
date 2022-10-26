using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyEshop.Data;
using MyEshop.Models;
using ZarinpalSandbox;

namespace MyEshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MyEshopContext _context;

        public HomeController(ILogger<HomeController> logger, MyEshopContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        public IActionResult Detail(int id)
        {
            var product = _context.Products.Include(p => p.Item).SingleOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var categories = _context.Products
                .Where(p => p.Id == id)
                .SelectMany(c => c.CategoryToProducts)
                .Select(ca => ca.Category)
                .ToList();

            var vm = new DetailsViewModel() { Product = product, Categories = categories };

            return View(vm);
        }

        [Authorize]
        public IActionResult RemoveFromCart(int DetailId)
        {
            var orderDetail = _context.orderDetails.Find(DetailId);
            if(orderDetail.Count>1)
            {
                orderDetail.Count--;
            }
            else
            {
            _context.Remove(orderDetail);
            }
            
            _context.SaveChanges();
            return RedirectToAction("ShowCart");
        }

        [Authorize]
        public IActionResult AddToCart(int itemId)
        {
            var product = _context.Products
                .Include(a => a.Item)
                .SingleOrDefault(a => a.ItemId == itemId);

            if (product != null)
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
                var order = _context.orders.FirstOrDefault(m => m.UserId == userId && !m.IsFinaly);

                if (order!=null)
                {
                    var orderDeatil= _context.orderDetails.FirstOrDefault(p=>p.OrderId==order.OrderId&&p.ProductId==product.Id);

                    if (orderDeatil!=null)
                    {
                        orderDeatil.Count++;
                    }
                    else
                    {
                        _context.orderDetails.Add(new OrderDetail()
                    {
                        OrderId=order.OrderId,
                        Price=product.Item.Price,
                        ProductId=product.Id,
                        Count=1
                    });
                    }
                }
                else
                {
                    order = new Order()
                    {
                        IsFinaly=false,
                        CreateDate=DateTime.Now,
                        UserId=userId
                    };
                    _context.orders.Add(order);
                    _context.SaveChanges();
                    _context.orderDetails.Add(new OrderDetail()
                    {
                        OrderId=order.OrderId,
                        Price=product.Item.Price,
                        ProductId=product.Id,
                        Count=1
                    });
                }
                _context.SaveChanges();
            }

            return RedirectToAction("ShowCart");
        }


        [Authorize]
        public IActionResult ShowCart()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var order = _context.orders.Where(p=>p.UserId==userId&&!p.IsFinaly)
                        .Include(o=>o.orderDetails)
                        .ThenInclude(p=>p.Product)
                        .ThenInclude(p=>p.Item).FirstOrDefault();
            if(order?.orderDetails.Count()<1){
                order=null;
            }
            return View(order);
        }

        [Route("ContactUs")]
        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public IActionResult Payment()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var order = _context.orders
                .Include(o=>o.orderDetails)
                .FirstOrDefault(o => o.UserId == userId && !o.IsFinaly);
            if (order == null)
                return NotFound();

            var payment = new Payment((int)order.orderDetails.Sum(d=>d.Price));
            var res = payment.PaymentRequest($"پرداخت فاکتور شماره {order.OrderId}",
                "http://localhost:5000/Home/OnlinePayment/" + order.OrderId, "Iman@Madaeny.com", "09197070750");
            if (res.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }
            else
            {
                return BadRequest();
            }

        }
        
        public IActionResult OnlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != ""&&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"&&
                HttpContext.Request.Query["Authority"] != "")
            {
                var authority= HttpContext.Request.Query["Authority"].ToString();
                var order=_context.orders.Include(p=>p.orderDetails).FirstOrDefault(p=>p.OrderId==id);
                var payment= new Payment((int)order.orderDetails.Sum(p=>p.Price));
                var res= payment.Verification(authority).Result;
                if (res.Status==100)
                {
                    order.IsFinaly=true;
                    _context.orders.Update(order);
                    _context.SaveChanges();
                    ViewBag.code=res.RefId;
                    return View();
                }  
                
            }
            return NotFound();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                }
            );
        }
    }
}
