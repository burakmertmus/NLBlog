using NLBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLBlog.Mvc.Areas.Admin.Model
{
    public class CategoryUpdateAjaxViewModel
    {
        public CategoryUpdateDto CategoryUpdateDto { get; set; }
        public string CategoryUpdatePartial { get; set; }
        public CategoryDto CategoryDto { get; set; }
    }
}
