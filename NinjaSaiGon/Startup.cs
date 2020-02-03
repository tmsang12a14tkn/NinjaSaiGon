using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NinjaSaiGon.Data;
using NinjaSaiGon.Data.Models;
using NinjaSaiGon.Services;
using Microsoft.AspNetCore.Http;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Repositories;
using NinjaSaiGon.Admin.Helpers;
using System.Globalization;
using AutoMapper;
using NinjaSaiGon.Admin;
using System.Runtime.InteropServices;
using System.IO;
using DinkToPdf;
using DinkToPdf.Contracts;
using NinjaSaiGon.Admin.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;

namespace NinjaSaiGon
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            var libwkhtmltoxFile = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "libwkhtmltox.dll" : RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "libwkhtmltox.so" : "libwkhtmltox.dylib";
            var assemblyLoadContext = new CustomAssemblyLoadContext();
            assemblyLoadContext.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), libwkhtmltoxFile));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 2;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(options => {
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.Events.OnSigningIn = async(signinContext) =>
                {
                    signinContext.Properties.ExpiresUtc = DateTimeOffset.Now.AddDays(30);
                    signinContext.CookieOptions.Expires = signinContext.Properties.ExpiresUtc?.ToUniversalTime();
                };
            });
            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromDays(30);
            });
            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = int.MaxValue;
            });

            services.AddAuthorization(options => options.AddPolicy("Admin", policy => policy.Requirements.Add(new SiteRequirement("admin"))));
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IDrinkRepository, DrinkRepository>();
            services.AddTransient<IToppingRepository, ToppingRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IPromotionRepository, PromotionRepository>();
            services.AddTransient<IPrivatePromotionRepository, PrivatePromotionRepository>();
            services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
            services.AddTransient<INotifyPopupRepository, NotifyPopupRepository>();
            services.AddTransient<IPersonLevelRepository, PersonLevelRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IAgencyRepository, AgencyRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IBusinessAreaRepository, BusinessAreaRepository>();
            services.AddTransient<ICommissionFormulaRepository, CommissionFormulaRepository>();
            services.AddTransient<IControllerActionRepository, ControllerActionRepository>();
            services.AddTransient<IOrderSourceTypeRepository, OrderSourceTypeRepository>();
            services.AddTransient<IOrderSourceRepository, OrderSourceRepository>();
            services.AddTransient<IDeliveryPartnerRepository, DeliveryPartnerRepository>();
            services.AddTransient<IGenericRepository<Device>, GenericRepository<Device>>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IAuthorizationHandler, RoleAuthorizeAttribute>();
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IPdfService, PdfService>();

            services.AddDistributedMemoryCache();
            services.AddAutoMapper();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            var cultureInfo = new CultureInfo("vi-VN");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            IdentityDataInitializer.SeedData(userManager, roleManager);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
