using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ShopBridge.Common;
using ShopBridge.DataLayer;
using System;

namespace ShopBridge
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
            services.AddControllersWithViews();
            services.AddDbContext<ShopBridgeDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DbConnection")));
            services.AddControllers();
            services.AddScoped<Configuration>(SetupConfig);
            SetupCorsPolicies(services);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopBridge", Version = "v1" });
            });
        }
        public void SetupCorsPolicies(IServiceCollection services)
        {
            var ServiceProvider = services.BuildServiceProvider();
            var corsPolicy = ServiceProvider.GetRequiredService<Configuration>();

            services.AddCors((corsOptions) => corsOptions.AddPolicy("CorsPolicy", optionBuilder =>
            {
                if (corsPolicy.AllowAnyHeader)
                {
                    optionBuilder.AllowAnyHeader();
                }

                if (corsPolicy.AllowAnyMethod)
                {
                    optionBuilder.AllowAnyMethod();
                }

                if (corsPolicy.AllowAnyOrigin)
                {
                    optionBuilder.AllowAnyOrigin();
                }

                if (!corsPolicy.AllowAnyOrigin && corsPolicy.AllowAnyOrigin)
                {
                    optionBuilder.AllowCredentials();
                }

                else
                {
                    optionBuilder.DisallowCredentials();
                }

                if (corsPolicy.WithExposedHeaders != null && corsPolicy.WithExposedHeaders.Count > 0)
                {
                    optionBuilder.WithExposedHeaders(corsPolicy.WithExposedHeaders.ToArray());
                }

                if (corsPolicy.WithHeaders != null && corsPolicy.WithHeaders.Count > 0)
                {
                    optionBuilder.WithHeaders(corsPolicy.WithHeaders.ToArray());
                }

                if (corsPolicy.WithMethods != null && corsPolicy.WithMethods.Count > 0)
                {
                    optionBuilder.WithMethods(corsPolicy.WithMethods.ToArray());
                }

                if (corsPolicy.WithOrigins != null && corsPolicy.WithOrigins.Count > 0)
                {
                    optionBuilder.WithOrigins(corsPolicy.WithOrigins.ToArray());
                }

            }));
        }

        private Configuration SetupConfig(IServiceProvider arg)
        {
            return new Configuration(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ShopBridgeDbContext db)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopBridge v1"));
            }

            db.Database.EnsureCreated();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            }); 
        }
    }
}
