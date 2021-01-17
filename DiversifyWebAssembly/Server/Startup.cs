using System;
using DiversifyWebAssembly.Server.Data;
using DiversifyWebAssembly.Server.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using DiversifyCL.Data;
using DiversifyCL.Interfaces.Repositories;
using DiversifyCL.Interfaces.Services;
using DiversifyCL.Repositories;
using DiversifyCL.Services;
using Syncfusion.Blazor;

namespace DiversifyWebAssembly.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Registering dbcontext
            services.AddDbContext<DiversifyContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            // Adding for application user
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Authentication 
            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            // Initializing API Config as string
            string stockSearchUri = Configuration["StockApi:BaseUri"];
            string newsUri = Configuration["NewsApi:BaseUri"];

            services.AddControllersWithViews();
            services.AddRazorPages();

            // Adding httpclient 
            services.AddHttpClient();

            // Registering httpclient service for stock searching
            services.AddHttpClient<IStockService, StockService>(client =>
            {
                client.BaseAddress = new Uri(stockSearchUri);
            });

            // Registering httpclient service for company news searching
            services.AddHttpClient<ICompanyNewsService, CompanyNewsService>(client =>
            {
                client.BaseAddress = new Uri(newsUri);
            });

            // Registering httpclient service for company overview 
            services.AddHttpClient<ICompanyOverviewService, CompanyOverviewService>(client =>
            {
                client.BaseAddress = new Uri(stockSearchUri);
            });

            // Registering services that do not need httpclient
            services.AddScoped<IStockPortfolioService, StockPortfolioService>();
            services.AddScoped<IInvestmentTotalService, InvestmentTotalService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ICompanyService, CompanyService>();

            // Registering repositories 
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<ISectorRepository, SectorRepository>();
            services.AddScoped<IInvestmentTotalRepository, InvestmentTotalRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();

            // Adding Syncfusion for Blazor
            services.AddSyncfusionBlazor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
