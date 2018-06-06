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
using Lang.Data;
using Lang.Models;
using Lang.Services;
using FluentValidation.AspNetCore;
using Lang.Hubs;

namespace Lang
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

            services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication().AddGoogle(x =>
            {
                x.ClientId = Configuration["oauth:google:clientId"];
                x.ClientSecret = Configuration["oauth:google:clientSecret"];
            });
            services.AddAuthentication().AddFacebook(x =>
            {
                x.AppId = Configuration["oauth:facebook:appId"];
                x.AppSecret = Configuration["oauth:facebook:appSecret"];
            });
            services.AddAuthentication().AddTwitter(x =>
            {
                x.ConsumerKey = Configuration["oauth:twitter:consumerKey"];
                x.ConsumerSecret = Configuration["oauth:twitter:consumerSecret"];
            });
            services.AddAuthentication().AddMicrosoftAccount(x =>
            {
                x.ClientId = Configuration["oauth:microsoft:applicationId"];
                x.ClientSecret = Configuration["oauth:microsoft:password"];
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMemoryCache();
            services.AddSession(x =>
            {
                x.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            services.AddMvc()
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddScoped<LangHub>();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSignalR(routes =>
            {
                routes.MapHub<LangHub>("/LangHub");
            });

            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(env.ContentRootPath, "App_Data"));
        }
    }
}
