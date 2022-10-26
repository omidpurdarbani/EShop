﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyEshop.Data;
using MyEshop.Models;

namespace MyEshop.Pages.Admin.ManageUsers
{
    public class CreateModel : PageModel
    {
        private readonly MyEshop.Data.MyEshopContext _context;

        public CreateModel(MyEshop.Data.MyEshopContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User Users { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
        

            _context.users.Add(Users);
            _context.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}