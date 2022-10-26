using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
 
namespace MyEshop.Models
{
    public class AddEditProductViewModel
    {
        
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public decimal Price { get; set; }
        
        public int QuantityInStock { get; set; }
        
        public IFormFile Picture { get; set; }

        public List<Category> Categories{get; set;} 
        public List<int> SelectedGroups{get; set;}
    }
}