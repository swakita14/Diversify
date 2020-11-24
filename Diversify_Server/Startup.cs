using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Diversify_Server.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Diversify_Server.Interfaces.Services;
using Diversify_Server.Interfaces.Repositories;
using Diversify_Server.Repositories;
using Diversify_Server.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Diversify_Server
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
            
            // Registering DbContext for application user
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<IdentityContext>();

            // Adding this for getting user information
            services.AddHttpContextAccessor();

            // Adding authentication
            services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

            // Adds cookie authentication service 
            services.AddAuthentication("Identity.Application").AddCookie();

            // Initializing as string
            string stockSearchUri = Configuration["StockApi:BaseUri"];
            string newsUri = Configuration["NewsApi:BaseUri"];
            services.AddRazorPages();
            services.AddServerSideBlazor();

            // Adding httpclient 
            services.AddHttpClient();

            // Registering httpclient service for stock searching
            services.AddHttpClient<IStockSearchService,StockSearchService>(client =>
            {
                client.BaseAddress = new Uri(stockSearchUri);
            });

            // Registering httpclient service for company news searching
            services.AddHttpClient<ICompanyNewsService,CompanyNewsService>(client =>
            {
                client.BaseAddress = new Uri(newsUri);
            });

            // Registering httpclient service for company overview 
            services.AddHttpClient<ICompanyOverviewService,CompanyOverviewService>(client =>
            {
                client.BaseAddress = new Uri(stockSearchUri);
            });

            // Registering services that do not need httpclient
            services.AddScoped<IStockPortfolioService, StockPortfolio>();

            // Registering repositories 
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<ISectorRepository, SectorRepository>();

            
            


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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
