using Microsoft.Extensions.DependencyInjection;
using NLBlog.Data.Abstract;
using NLBlog.Data.Concrete;
using NLBlog.Data.Concrete.EntitiyFramework.Contexts;
using NLBlog.Services.Abstract;
using NLBlog.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLBlog.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<NLBlogContext>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IArticleService, ArticleManager>();
            return serviceCollection;


        }
    }
}
