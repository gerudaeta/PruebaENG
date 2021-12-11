using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation.AspNetCore;
using PruebaENG.Api.Filters;
using PruebaENG.Api.Swagger;
using PruebaENG.Application;
using PruebaENG.Infrastructure;
using PruebaENG.Infrastructure.Persistence;

namespace PruebaENG.Api
{
    public class Startup
    {
        readonly string _corsAllowedOrigins = "AllowSpecificOrigins";
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: _corsAllowedOrigins,
                    builder =>
                    {
                        var origins = Configuration.GetValue<string>("CorsOrigins").Split(",");
                        builder.WithOrigins(origins)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            
            services.AddDatabaseDeveloperPageExceptionFilter();
            
            services.AddHttpContextAccessor();

            services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();

            services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation();
            
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddSwagger();
            
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PruebaENG.Api v1");
                    c.RoutePrefix = string.Empty;
                });
            } else {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHealthChecks("/health");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();
            
            app.UseCors(_corsAllowedOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
