using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyEshop.Data;
using MyEshop.Models;

namespace MyEshop.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Product> products { get; set; }

        private MyEshopContext _context;

        public IndexModel(MyEshopContext Context)
        {
            _context = Context;
        }

        public void OnGet() 
        {
            products = _context.Products.Include(p=>p.Item);
        }

        public void OnPost() 
        { 
            
        }
    }
}
