using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLBlog.Mvc.Controllers
{
    public class NotFoundController : Controller
    {
        
       
       public ViewResult Index()
        {
            return View("_PageNotFound");
        }
    }
}
