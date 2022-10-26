using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop.Data;
using MyEshop.Models;

namespace EShop.Pages.Admin
{
    public class AddModel : PageModel
    {
        private MyEshopContext _context;

        public AddModel(MyEshopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AddEditProductViewModel Product { get; set; }

        [BindProperty]
        public List<int> SelectedGroups{get; set;}
        public void OnGet()
        {
            Product = new AddEditProductViewModel() 
            { 
                Categories = _context.Categories.ToList() 
            };
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var item = new Item()
            {
                Price = Product.Price,
                QuantityInStock = Product.QuantityInStock
            };
            _context.Add(item);
            _context.SaveChanges();

            var product = new Product()
            {
                Name = Product.Name,
                Description = Product.Description,
                Item = item
            };

            _context.Add(product);
            _context.SaveChanges();
            _context.Products.Single(p => p.Id == product.Id).ItemId = item.Id;
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
