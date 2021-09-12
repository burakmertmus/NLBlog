using NLBlog.Entities.Concrete;
using NLBLog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLBlog.Entities.Dtos
{
    public class CategoryDto:DtoGetBase
    {
        public Category Category { get; set; }
    }
}
