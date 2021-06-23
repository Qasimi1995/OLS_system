using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using OLS.Models;
using Microsoft.AspNetCore.Identity;
using OLS.FunctionsLibrary;
using EmailService;
using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

namespace OLS
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
            var emailConfig = Configuration 
            .GetSection("EmailConfiguration")
            .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
               opt.TokenLifespan = TimeSpan.FromHours(2));

            services.AddAuthentication();
            services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.TopLeft; });
            services.AddIdentity<User, IdentityRole>(
                options =>
                {
                    // Default Password settings.
                    options.Password.RequireDigit = false;
                   // options.Password.RequiredLength = 10;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    // options.Password.RequiredLength = 10;
                    // options.Password.RequiredUniqueChars = 3;  
                    //opt.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();
                services.AddDbContext<ApplicationContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("MyConnection")));

            //services.Configure<IdentityOptions>(options =>
            //{
            //    // Default Password settings.
            //    options.Password.RequireDigit = false;
            //    options.Password.RequiredLength = 10;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = false;
            //    // options.Password.RequiredLength = 10;
            //    // options.Password.RequiredUniqueChars = 3;

            //    //opt.SignIn.RequireConfirmedEmail = true;
            //});

            services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
            services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(opt =>
            {
                var supportedCultures = new List<CultureInfo>()
                {
                     new CultureInfo("en"),
                     new CultureInfo("ps"),
                     new CultureInfo("fa")
                };

                opt.DefaultRequestCulture = new RequestCulture("en");
                opt.SupportedCultures = supportedCultures;
                opt.SupportedUICultures = supportedCultures;

            });


            services.AddControllersWithViews();

            services.AddDistributedMemoryCache();
            services.AddSession((options)=> options.IdleTimeout = TimeSpan.FromMinutes(300));
            services.AddTransient(typeof(Functions));
            




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else 
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();


         
            app.UseRouting();
            
            app.UseAuthorization();


            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);


            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
