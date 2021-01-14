using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using DiversifyCL.Interfaces.Services;
using DiversifyCL.Services;
using DiversifyHangFire.Data;
using DiversifyHangFire.Interfaces.Repositories;
using DiversifyHangFire.Interfaces.Services;
using DiversifyHangFire.Repositories;
using DiversifyHangFire.Services;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DiversifyHangFire
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Registering dbcontext
            services.AddDbContext<HangFireDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            // Initialize String for API
            string stockSearchUri = Configuration["StockApi:BaseUri"];

            // Registering httpclient service for company overview 
            services.AddHttpClient<ICompanyOverviewService, CompanyOverviewService>(client =>
            {
                client.BaseAddress = new Uri(stockSearchUri);
            });

            // Adding Repository
            services.AddScoped<ICompanyInformationRepository, CompanyInformationRepository>();

            // Adding Service
            services.AddScoped<IOverviewUpdateService, OverviewUpdateService>();

            // Adding HangFire 
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration["ConnectionStrings:HangFireConnection"]));
            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Adding HangFire Dashboard 
            app.UseHangfireDashboard();

            //Entering Recurring Job executing minutely
            RecurringJob.AddOrUpdate<IOverviewUpdateService>(x => x.UpdateCompanyOverview(), Cron.Minutely);

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
