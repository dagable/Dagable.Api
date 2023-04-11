using Dagable.Api.Models;
using Dagable.Api.Pipeline.ActionFilters;
using Dagable.ErrorManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Dagable.Api.Controllers
{
    [IncomingRequestModelValidationActionFilter]
    public class BaseController<T> : ControllerBase
    {
        private readonly ILogger<T> _logger;

        public BaseController() { }

        public BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }

        public override UnauthorizedObjectResult Unauthorized(object? error)
        {
            var response = new DagableStandardResult(null);
            return new UnauthorizedObjectResult(response);
        }

        public virtual ObjectResult Forbidden(DagableError error)
        {
            var response = new DagableStandardResult(null, true, error);
            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status403Forbidden,
            };
        }

        public override OkObjectResult Ok(object? content)
        {
            var response = new DagableStandardResult(content);
            return new OkObjectResult(response);
        }
    }
}
