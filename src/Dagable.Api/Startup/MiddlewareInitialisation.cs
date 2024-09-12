using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Dagable.Api.Startup
{
    public static partial class MiddlewareInitialisation
    {
        public static WebApplication ConfigureMiddleware(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(builder =>
                builder
                   .WithOrigins(app.Configuration.GetValue<string>("AppUrl").Split(','))
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials()
               );

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
