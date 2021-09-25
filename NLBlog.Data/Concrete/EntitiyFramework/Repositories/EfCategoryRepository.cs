using Microsoft.EntityFrameworkCore;
using NLBlog.Data.Abstract;
using NLBlog.Data.Concrete.EntitiyFramework.Contexts;
using NLBlog.Entities.Concrete;
using NLBlog.Shared.Data.Concrete.EntityFramework;
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

        //Custom Methods
        public async Task<Category> GetById(int categoryId)
        {
            return await NLBlogContext.Categories.SingleOrDefaultAsync(c=>c.Id==categoryId);
        }
        private NLBlogContext NLBlogContext
        {
            get
            {
                return _context as NLBlogContext;
            }
        }
    }
}
