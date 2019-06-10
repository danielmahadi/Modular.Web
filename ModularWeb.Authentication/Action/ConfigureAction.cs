using System;
using System.Threading.Tasks;
using AspNetCore.Identity.MongoDbCore.Models;
using ExtCore.Infrastructure.Actions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MongoDbGenericRepository.Attributes;

namespace ModularWeb.Authentication.Action
{
    public class ConfigureAction : IConfigureAction
    {
        public int Priority => 1000;

        public void Execute(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            app.UseAuthentication();
        }
    }
    
    public class ConfigureServicesAction : IConfigureServicesAction
    {
        public int Priority => 1000;

        public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
        {
            var mongodbConnectionString = Environment.GetEnvironmentVariable("IDENTITYCORE_MONGO_DB_CONNECTIONSTRING");
            var databaseName = Environment.GetEnvironmentVariable("IDENTITYCORE_MONGO_DB_DATABASE");
                
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(mongodbConnectionString, databaseName)
                .AddDefaultTokenProviders();
            
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = Environment.GetEnvironmentVariable("IDENTITYCORE_COOKIES_NAME");
                options.Cookie.Expiration = TimeSpan.FromDays(30);
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.SlidingExpiration = true;
                options.Cookie.SameSite = SameSiteMode.None;

                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });
            
            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                // This is the key to control how often validation takes place
                options.ValidationInterval = TimeSpan.FromMinutes(5);
            });
        }
    }
    
    [CollectionName("Roles")]
    public class ApplicationRole : MongoIdentityRole<Guid>
    {
        public ApplicationRole() : base()
        {
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
    
    [CollectionName("Users")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
        public ApplicationUser() : base()
        {
        }

        public ApplicationUser(string userName, string email) : base(userName, email)
        {
        }
    }
}