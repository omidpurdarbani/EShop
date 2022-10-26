using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyEshop.Data;
using MyEshop.Models;

namespace MyEshop.Controllers
{
    public class ProductController : Controller
    {

        private MyEshopContext _context;
        public ProductController(MyEshopContext context)
        {
            _context=context;
        }



        [Route("Group/{id}/{name}")]
        public IActionResult ShowProductsByGroupId(int id,string name)
        {
            var products= _context.CategoryToProducts
            .Where(p=>p.CategoryId==id)
            .Select(s=>s.Product)
            .ToList();
            ViewData["GroupName"]=name;
            return View(products);
        }
    }
}
