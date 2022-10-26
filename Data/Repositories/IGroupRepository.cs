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

namespace Repositories
{
    public interface IGroupRepository
    {  
        IEnumerable<Category> ShowAllGroups();
        IEnumerable<GroupViewModel> GroupViewModelForShow();
    }

    public class GroupRepository : IGroupRepository
    {
        MyEshopContext _context;

        public GroupRepository(MyEshopContext context)
        {
            _context=context;
        }
        public IEnumerable<GroupViewModel> GroupViewModelForShow()
        {
            return _context.Categories
            .Select(a=>new GroupViewModel{
                  GroupID=a.Id,
                  GroupName=a.Name,
                  ProductCount=_context.CategoryToProducts
                               .Count(c=>c.CategoryId==a.Id)
            }).ToList();
        }

        public IEnumerable<Category> ShowAllGroups()
        {
            return _context.Categories.ToList();
        }
    }
}