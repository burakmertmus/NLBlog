using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLBlog.Entities.Concrete;
using NLBlog.Mvc.Areas.Admin.Model;
using NLBlog.Services.Abstract;
using NLBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Editor")]
    public class HomeController : Controller
    {

        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;
        private readonly ICommentService _commentService;
        private readonly UserManager<User> _usermanager;

        public HomeController(ICategoryService categoryService, IArticleService articleService, ICommentService commentService, UserManager<User> usermanager)
        {
            _categoryService = categoryService;
            _articleService = articleService;
            _commentService = commentService;
            _usermanager = usermanager;
        }

        
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
            var categoriesCountResult = await _categoryService.CountByIsDeleted();
            var articlesCountResult = await _articleService.CountByIsDeleted();
            var commentsCountResult = await _commentService.CountByIsDeleted();
            var userCount = await _usermanager.Users.CountAsync();

            var articlesResult = await _articleService.GetAll();

            if (categoriesCountResult.ResultStatus==ResultStatus.Success&&
                articlesCountResult.ResultStatus == ResultStatus.Success &&
                commentsCountResult.ResultStatus == ResultStatus.Success &&
                userCount>-1 &&
                articlesResult.ResultStatus == ResultStatus.Success)
            {
                return View(new DashboardViewModel
                {
                    CategoriesCount = categoriesCountResult.Data,
                    ArticlesCount= articlesCountResult.Data,
                    CommentsCount=commentsCountResult.Data,
                    UsersCount=userCount,
                    Articles=articlesResult.Data
                }) ;
            }
            return NotFound();
        }


    }
}
