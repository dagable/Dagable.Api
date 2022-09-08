using Dagable.Api.Services;
using Dagable.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;

namespace Dagable.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            // adds DI services to DI and configures bearer as the default scheme
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    // identity server issuing token
                    options.Authority = Configuration.GetValue<string>("Authority");
                    options.RequireHttpsMetadata = true;
                    // allow self-signed SSL certsnpm 
                    options.BackchannelHttpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = delegate { return true; } };

                    // the scope id of this api
                    options.Audience = "dagableApi";
                });

            services.AddMvcCore(options =>
            {
                options.EnableEndpointRouting = false;
            })
            .AddAuthorization();

            services.AddScoped<IDagServices, DagServices>();
            services.AddDagableCoreServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                 builder
                   .WithOrigins(Configuration.GetValue<string>("AppUrl").Split(','))
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials()
               );

            app.UseHttpsRedirection();

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
