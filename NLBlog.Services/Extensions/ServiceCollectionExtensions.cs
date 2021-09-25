
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLBlog.Data.Abstract;
using NLBlog.Data.Concrete;
using NLBlog.Data.Concrete.EntitiyFramework.Contexts;
using NLBlog.Entities.Concrete;
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
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection,string connectionString)
        {

           serviceCollection.AddDbContext<NLBlogContext>(options=>options.UseSqlServer(connectionString));
            //serviceCollection.AddIdentity<User,Role>(options => {
            //     //User Password Options
            //     options.Password.RequireDigit=false;
            //     options.Password.RequiredLength = 5;
            //     options.Password.RequiredUniqueChars=2;
            //     options.Password.RequireNonAlphanumeric=false;
            //     options.Password.RequireLowercase = false;
            //     options.Password.RequireUppercase = false;
            //     //UserName and Email Options
            //     options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            //     options.User.RequireUniqueEmail = true;
            //     options.SignIn.RequireConfirmedAccount = false;
            // }).AddEntityFrameworkStores<NLBlogContext>();

            serviceCollection.AddIdentity<User, Role>(options =>
            {
                // User Password Options
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                // User Username and Email Options
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+$";
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<NLBlogContext>();


            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IArticleService, ArticleManager>();
            
            return serviceCollection;


        }
    }
}
