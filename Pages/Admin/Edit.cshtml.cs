using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyEshop.Data;
using MyEshop.Models;

namespace EShop.Pages.Admin
{
    public class EditModel : PageModel
    {
        private MyEshopContext _context;

        public EditModel(MyEshopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AddEditProductViewModel Product { get; set; }

        [BindProperty]
        public List<int> SelectedGroups { get; set; }

        public List<int> GroupsProduct { get; set; }
        

        public void OnGet(int id)
        {
            Product = _context.Products
                .Include(p => p.Item)
                .Where(p => p.Id == id)
                .Select(
                    p =>
                        new AddEditProductViewModel()
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description,
                            Price = p.Item.Price,
                            QuantityInStock = p.Item.QuantityInStock
                        }
                )
                .FirstOrDefault();
                Product.Categories = _context.Categories.ToList();
            GroupsProduct=_context.CategoryToProducts.Where(p=>p.ProductId==id).Select(p=>p.CategoryId).ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var product = _context.Products.FirstOrDefault(p => p.Id == Product.Id);
            var item = _context.Items.FirstOrDefault(p => p.Id == product.ItemId);

            product.Name = Product.Name;
            product.Description = Product.Description;
            item.Price = Product.Price;
            item.QuantityInStock = Product.QuantityInStock;

            _context.SaveChanges();

            if (Product.Picture?.Length > 0)
            {
                string FilePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "images",
                    product.Id + ".jpg"
                );

                using (var stream = new FileStream(FilePath, FileMode.Create))
                {
                    Product.Picture.CopyTo(stream);
                }
                ;
            }

            _context.CategoryToProducts.Where(p=>p.ProductId==product.Id).ToList()
            .ForEach(p=>_context.CategoryToProducts.Remove(p));

            if(SelectedGroups.Any()&& SelectedGroups.Count()>0)
            {
                foreach (var gr in SelectedGroups)
                {
                    _context.CategoryToProducts.Add(new CategoryToProduct()
                    {
                        ProductId=product.Id,
                        CategoryId=gr
                    });
                }
                _context.SaveChanges();
            }
            return RedirectToPage("Index");
        }
    }
}
