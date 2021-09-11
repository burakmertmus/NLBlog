using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NLBLog.Shared.Abstract;
namespace NLBLog.Shared.Data.Abstact
{
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        Task<T> GetAsync(Expression<Func<T,bool>> predicate,params Expression<Func<T,object>>[] includeProperties); //var kullanıcı = repository.GetAsync(p=>p.Id=12)

        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicateİ=null,
            params Expression<Func<T, object>>[] includeProperties);
        Task AddAsync (T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> AnyAsync (Expression<Func<T, bool>> predicate);
        Task<int> CountAsync (Expression<Func<T, bool>> predicate);

    }
}
