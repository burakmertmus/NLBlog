using NLBlog.Data.Abstract;
using NLBlog.Data.Concrete.EntitiyFramework.Contexts;
using NLBlog.Data.Concrete.EntitiyFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLBlog.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NLBlogContext _context;
        private readonly EfArticleRepository _articleRepository;
        private readonly EfCategoryRepository _categoryRepository;
        private readonly EfCommentRepository _commentRepository;

        public UnitOfWork(NLBlogContext context)
        {
            _context = context;
        }

        public IArticleRepository Articles => _articleRepository ?? new EfArticleRepository(_context);

        public ICategoryRepository Categories => _categoryRepository ?? new EfCategoryRepository(_context);

        public ICommentRepository Comments => _commentRepository ?? new EfCommentRepository(_context);

       
        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
