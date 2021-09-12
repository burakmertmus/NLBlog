using Microsoft.EntityFrameworkCore;
using NLBlog.Data.Abstract;
using NLBlog.Entities.Concrete;
using NLBLog.Shared.Data.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLBlog.Data.Concrete.EntitiyFramework.Repositories
{
    public class EfCategoryRepository : EfEntityRepositoryBase<Category>, ICategoryRepository
    {
        public EfCategoryRepository(DbContext context) : base(context)
        {

        }
    }
}
