using Microsoft.EntityFrameworkCore;
using NLBlog.Data.Abstract;
using NLBlog.Entities.Concrete;
using NLBLog.Shared.Abstract.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLBlog.Data.Concrete.EntitiyFramework.Repositories
{
    public class EfArticleRepository : EfEntityRepositoryBase<Article>, IArticleRepository
    {
        public EfArticleRepository(DbContext context) : base(context)
        {

        }
    }
}
