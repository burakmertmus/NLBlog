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
    class EfRoleRepository : EfEntityRepositoryBase<Role>, IRoleRepository
    {
        public EfRoleRepository(DbContext context) : base(context)
        {

        }
    }
}
