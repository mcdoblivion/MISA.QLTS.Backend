using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using MISA.Common.Models;
using MISA.Common.Properties;
using MISA.DataLayer.DbContexts;
using MISA.DataLayer.Interfaces;
using MISA.Service;
using MISA.Service.Interfaces;
using MISA.Service.Services;

namespace MISA.QLTS.Api
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MISA.QLTS.Api", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            // Cấu hình DI
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<IAssetService, AssetService>();
            //services.AddScoped<ICustomerService, CustomerService>();
            //services.AddScoped<ICustomerService, CustomerServiceV2>();

            services.AddScoped(typeof(IDbContext<>), typeof(DbContext<>));
            services.AddScoped<IAssetDbContext, AssetDbContext>();
            //services.AddScoped(typeof(IDbContext<>), typeof(DbContextV2<>));
            //services.AddScoped<ICustomerDbContext, CustomerDbContext>();
            //services.AddScoped<ICustomerDbContext, CustomerDbContextV2>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MISA.QLTS.Api v1"));
            }

            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;
                var errorMsg = new ErrorMsg()
                {
                    DevMsg = exception.Message,
                };
                errorMsg.UserMsg.Add(Resources.ErrorService_General);
                await context.Response.WriteAsJsonAsync(errorMsg);
            }));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}