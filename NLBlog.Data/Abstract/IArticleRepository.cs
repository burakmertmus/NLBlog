using NLBlog.Entities.Concrete;
using NLBlog.Shared.Data.Abstact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLBlog.Data.Abstract
{
    public interface IArticleRepository:IEntityRepository<Article>
    {
        
    }
}
