using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Repository;
using Domain.Repository.Impl;
using InputApi.Services;
using InputApi.Services.Impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace InputApi
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
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "InputApi", Version = "v1"}); });

            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            
            services.AddDbContext<ApplicationContext>(options => options
                .UseNpgsql(connectionString));
            
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IApplicationRepository, ApplicationRepository>();
            services.AddTransient<IApplicationDetailsRepository, ApplicationDetailsRepository>();
            
            services.AddSingleton<Repository.Mapper.IDataMapper, Repository.Mapper.DataMapper>();

            services.AddTransient<IApplicationService, ApplicationService>();
            services.AddTransient<ITaskSenderService, RpcTaskSenderService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InputApi v1"));
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}