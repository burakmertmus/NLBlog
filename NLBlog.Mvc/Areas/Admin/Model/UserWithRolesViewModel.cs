using NLBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLBlog.Mvc.Areas.Admin.Model
{
    public class UserWithRolesViewModel
    {
        public User User { get; set; }
        public IList<string> Roles { get; set; }
    }
}
