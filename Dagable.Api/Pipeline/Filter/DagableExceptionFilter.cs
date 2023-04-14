using Dagable.Api.Models;
using Dagable.ErrorManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static Dagable.ErrorManagement.ErrorManager;

namespace Dagable.Api.Pipeline.Filter
{
    /// <summary>
    /// An Exception filter that will catch any unhandled exceptions and properly format the response logging
    /// and masking the true error.
    /// </summary>
    public class DagableExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext exceptionContext)
        {
            var errorManager = exceptionContext.HttpContext.RequestServices.GetService<IDagableErrorManager>();
            var logger = exceptionContext.HttpContext.RequestServices.GetService<ILogger<DagableExceptionFilter>>(); 
            logger.LogError($"An Error occured that was unhandled: {exceptionContext.Exception}");
            exceptionContext.Result = new ObjectResult(new DagableStandardResult(null, true, errorManager[ErrorCodes.INTERNAL_SERVER_ERROR]))
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }
    }
}
