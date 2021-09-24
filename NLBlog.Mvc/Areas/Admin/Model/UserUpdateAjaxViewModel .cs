using NLBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLBlog.Mvc.Areas.Admin.Model
{
    public class UserUpdateAjaxViewModel
    {
        public UserUpdateDto UserUpdateDto { get; set; }
        public string UserUpdatePartial { get; set; }
        public UserDto UserDto { get; set; }
    }
}
