using NLBlog.Entities.Concrete;
using NLBlog.Shared.Entities.Abstract;
using NLBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLBlog.Entities.Dtos
{
    public class ArticleListDto:DtoGetBase
    {
        public IList<Article> Articles { get; set; }
        
    }
}
