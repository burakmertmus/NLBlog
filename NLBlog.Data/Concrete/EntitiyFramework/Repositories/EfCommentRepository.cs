using Microsoft.EntityFrameworkCore;
using NLBlog.Data.Abstract;
using NLBlog.Entities.Concrete;
using NLBlog.Shared.Data.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLBlog.Data.Concrete.EntitiyFramework.Repositories
{
    class EfCommentRepository : EfEntityRepositoryBase<Comment>, ICommentRepository
    {
        public EfCommentRepository(DbContext context) : base(context)
        {

        }
    }
}
