using Dagable.Api.Models;
using Dagable.ErrorManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using static Dagable.ErrorManagement.ErrorManager;

namespace Dagable.Api.Pipeline.Filter
{
    public class DagableExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext exceptionContext)
        {
            var exceptionType = exceptionContext.Exception.GetType();
            var errorManager = exceptionContext.HttpContext.RequestServices.GetService<IDagableErrorManager>();
            var logger = exceptionContext.HttpContext.RequestServices.GetService<ILogger<DagableExceptionFilter>>(); 

            if (exceptionType == typeof(ValidationException))
            {
                var exception = exceptionContext.Exception as ValidationException;
            }
            logger.LogError($"An Error occured that was unhandled: {exceptionContext.Exception}");
            exceptionContext.Result = new ObjectResult(new DagableStandardResult(null, true, errorManager[ErrorCodes.INTERNAL_SERVER_ERROR]))
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }
    }
}
