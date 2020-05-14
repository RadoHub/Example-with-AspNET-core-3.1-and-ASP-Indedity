using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prj.Bus.Abstract;
using Prj.Bus.Concrete;
using Prj.Dal.Abstract;
using Prj.Dal.Concrete.EF;
using Prj.WebUI.Extensions;
using Prj.WebUI.Identity;
using Prj.WebUI.EmailServices;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Prj.WebUI
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<ApplicationIdentityDbContext>(options=>options.UseSqlServer(Configuration.GetConnectionString("IdentityConnectionString")));
            services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<ApplicationIdentityDbContext>().AddDefaultTokenProviders();
            //  services.AddIdentity<ApplicationRole, >

            services.Configure<IdentityOptions>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(180);
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedAccount = false;
            });
            services.ConfigureApplicationCookie(options=> {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessdenied";
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                options.SlidingExpiration = true;
                options.Cookie = new CookieBuilder {

                    HttpOnly = true, 
                    Name="ShopApp.Security.Cookie", 
                    SameSite = SameSiteMode.Strict 
                };
                
                
            
            });
            services.AddSingleton<ICategoryDal, EFCategoryDal>();
            services.AddSingleton<ICategoryService, CategoryManager>();

            services.AddScoped<IProductDal, EFProductDal>();
            services.AddScoped<IProductService, ProductManager>();

            services.AddScoped<IProductCategoryDal, EFProductCategory>();
            services.AddScoped<IProductCategoryService, ProductCategoryManager>();

            services.AddScoped<ICartDal, EFCartDal>();
            services.AddScoped<ICartService, CartManager>();

            services.AddScoped<IOrderDal, EFOrderDal>();
            services.AddScoped<IOrderService, OrderManager>();

            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddTransient<IEmailSender, MVCEmailSender>();
        }
        

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedDataBase.SeedData();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseNpmStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
               

                endpoints.MapControllerRoute(name: "orders", pattern:"orders", defaults:new { controller= "Cart", action = "GetOrders" });

                endpoints.MapControllerRoute(name: "checkout", pattern: "checkout", defaults: new { controller = "cart", action = "checkout" });
                endpoints.MapControllerRoute(name: "Cart", pattern: "cart", defaults: new { controller = "cart", action = "index" }); 
                endpoints.MapControllerRoute(name:"default", pattern:"{controller}/{action}/{id?}", defaults:new { controller="home" , action="index"});

            });

            IdentityDataSeed.SeedIdentityData(userManager, roleManager, Configuration).Wait();
        }
    }
}
