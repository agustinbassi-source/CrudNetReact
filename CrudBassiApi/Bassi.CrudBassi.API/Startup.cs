using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Bassi.API.Configuration.Services;
using Bassi.API.Utilities;
using Bassi.API.Configuration.Filters;
using Bassi.API.Configuration;
using Bassi.API.Configuration.Logger;
using Bassi.CrudBassi.API.Dependencies;
using Bassi.ADO.Utilities;
using Bassi.CrudBassi.Repository;
using Microsoft.EntityFrameworkCore;

namespace Bassi.CrudBassi.Api
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
            services.AddSingleton<IAppSettings, AppSettings>();

            Microsoft.Extensions.DependencyInjection.ServiceProvider appSettingsService = services.BuildServiceProvider();

            services.AddRepositories(appSettingsService.GetService<IAppSettings>());

            services.AddJWTAuthentication(Configuration);

            services.AddManagers();

            services.AddCors(options => options.AddDefaultPolicy(p =>
            {
                p.WithOrigins(new string[] { "*" }).AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build();
            }));

          
            services.AddControllers();

            services.AddDbContext<DbContextEf>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("CrudBassi")));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options => options.WithOrigins(new string[] { "*" }).AllowAnyMethod().AllowAnyHeader().AllowAnyHeader().Build());

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

