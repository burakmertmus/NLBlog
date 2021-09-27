using NLBlog.Entities.Concrete;
using NLBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLBlog.Mvc.Areas.Admin.Model
{
    public class DashboardViewModel
    {
        public int CategoriesCount { get; set; }

        public int ArticlesCount { get; set; }

        public int CommentsCount { get; set; }

        public int UsersCount { get; set; }

        public ArticleListDto Articles { get; set; }

    }
}
