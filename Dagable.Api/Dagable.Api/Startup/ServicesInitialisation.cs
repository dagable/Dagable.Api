using Dagable.Api.Models;
using Dagable.Api.Pipeline.ActionFilters;
using Dagable.Api.Pipeline.Filter;
using Dagable.Api.Services;
using Dagable.Api.Services.Graphs;
using Dagable.Core;
using Dagable.Core.Scheduling;
using Dagable.DataAccess;
using Dagable.DataAccess.Migrations;
using Dagable.ErrorManagement;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using static Dagable.ErrorManagement.ErrorManager;

namespace Dagable.Api.Startup
{
    public static partial class ServicesInitialisation
    {
        public static IServiceCollection RegisterApplicationServices(
                                        this IServiceCollection services, WebApplicationBuilder builder)
        {
            RegisterCustomDependencies(services);
            RegisterSwagger(services);
            RegisterAuthDepenedencies(services, builder);
            RegisterDatabaseContextDependencies(services, builder);
            RegisterLoggingDependencies(services, builder);
            return services;
        }

        private static void RegisterCustomDependencies(IServiceCollection services)
        {
            services
               .AddScoped<IDagGenerationServices, GraphGenerationServices>()
               .AddScoped<IDagScheduleServices, GraphSchedulingServices>()
               .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
               .AddScoped<IUserRepository, UserRepository>()
               .AddScoped<IUserServices, UserServices>()
               .AddScoped<IGraphServices, GraphServices>()
               .AddScoped<IGraphsRepository, GraphsRepository>()
               .AddScoped<IDagableErrorManager, DagableErrorManager>()
               .AddDagableCoreServices()
               .AddDagableSchedulingServices()
               .AddControllers(opt =>
               {
                   opt.Filters.Add(typeof(DagableExceptionFilter));
               });

            //ignores the InvalidModelStateResponseFactory 
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        private static void RegisterAuthDepenedencies(IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddCors()
                    .AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                    };
                    // identity server issuing token
                    options.Authority = builder.Configuration.GetValue<string>("Authority");
                    options.RequireHttpsMetadata = false;
                    // allow self-signed SSL certsnpm 
                    options.BackchannelHttpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = delegate { return true; } };
                    // the scope id of this api
                    options.Audience = "dagableApi";
                });
        }

        private static void RegisterDatabaseContextDependencies(IServiceCollection services, WebApplicationBuilder builder)
        {
            var migrationsAssembly = typeof(DagableDbContext).GetTypeInfo().Assembly.GetName().Name;
            string mySqlConnectionStr = builder.Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DagableDbContext>(opt =>
            {
                opt.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr), sql => sql.MigrationsAssembly(migrationsAssembly));
                opt.UseMySql(ServerVersion.AutoDetect(mySqlConnectionStr), b => b.SchemaBehavior(MySqlSchemaBehavior.Translate, (schema, entity) => $"{schema ?? "dbo"}_{entity}"));
            });
        }

        private static void RegisterLoggingDependencies(IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddNLog(builder.Configuration);
            });
        }
        private static void RegisterSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Dagable API",
                    Description = "An API to support the creation of directed acyclic graphs and scheduling",
                    Contact = new OpenApiContact
                    {
                        Name = "John",
                        Url = new Uri("https://jwm.xyz")
                    }
                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            }
            );
        }
    }
}
