using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLBlog.Entities.Concrete;
using NLBlog.Shared.Data.Abstact;
namespace NLBlog.Data.Abstract
{
   public interface IUserRepository:IEntityRepository<User>
    {
    }
}
