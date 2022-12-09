using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Dagable.Api.Startup
{
    public static partial class EndpointInitialisation
    {
        public static WebApplication RegisterEndpoints(this WebApplication app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app; 
        }
    }
}
