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
    public class DeleteModel : PageModel
    {
        private MyEshopContext _context;

        public DeleteModel(MyEshopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AddEditProductViewModel Product { get; set; }

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
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var product = _context.Products.FirstOrDefault(p => p.Id == Product.Id);
            var item = _context.Items.FirstOrDefault(p => p.Id == product.ItemId);

            _context.Remove(item);
            _context.Remove(product);
            _context.SaveChanges();

            string FilePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "images",
                    product.Id + ".jpg"
                );
                
            if(System.IO.File.Exists(FilePath))
            {
                System.IO.File.Delete(FilePath);
            }
            return RedirectToPage("Index");
        }
    }
}
