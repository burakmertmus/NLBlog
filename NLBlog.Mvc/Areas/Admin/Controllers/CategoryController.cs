using Microsoft.AspNetCore.Mvc;
using NLBlog.Entities.Dtos;
using NLBlog.Mvc.Areas.Admin.Model;
using NLBlog.Services.Abstract;
using NLBlog.Shared.Utilities.Extensions;
using NLBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace NLBlog.Mvc.Areas.Admin.Controllers
{
    [Area(areaName:"Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAll();
            return View(result.Data);
        }
        public IActionResult Add()
        {
            
            return PartialView("_CategoryAddPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.Add(categoryAddDto,"Burak Mert Muş");
                if (result.ResultStatus==ResultStatus.Success)
                {
                    var categoryAddAjaxModel = JsonSerializer.Serialize(new CategoryAddAjaxViewModel { 
                        CategoryDto = result.Data,
                        CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial",categoryAddDto)
                    });
                    return Json(categoryAddAjaxModel);
                }
            }

            var categoryAddAjaxErrorModel = JsonSerializer.Serialize(new CategoryAddAjaxViewModel
            {
                CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial", categoryAddDto)
            });

            return Json(categoryAddAjaxErrorModel);

           
        }

    }
}
