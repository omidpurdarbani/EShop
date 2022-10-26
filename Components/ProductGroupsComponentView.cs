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
using Repositories;

namespace MyEshop.Components
{
    public class ProductGroupsComponentView : ViewComponent
    {
        private IGroupRepository _IGroupRepository;

        public ProductGroupsComponentView(IGroupRepository GroupRepository)
        {
            _IGroupRepository = GroupRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(
                "/Views/ComponentsView/Categories.cshtml",
                _IGroupRepository.GroupViewModelForShow()
            );
        }
    }
}
