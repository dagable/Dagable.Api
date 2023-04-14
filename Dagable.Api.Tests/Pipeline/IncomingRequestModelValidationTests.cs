using Dagable.Api.Pipeline.ActionFilters;
using Dagable.ErrorManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;

namespace Dagable.Api.Tests
{
    [TestClass]
    public class IncomingRequestModelValidationTests
    {
        private Mock<IServiceProvider> _serviceProvider;

        [TestInitialize]
        public void Initialise()
        {
            _serviceProvider = new Mock<IServiceProvider>();
            _serviceProvider
               .Setup(x => x.GetService(typeof(IDagableErrorManager)))
               .Returns(new DagableErrorManager());
        }

        [TestMethod]
        public void ModelValidation_WhenAValidErrorModel_ReturnsNoErrors()
        {
            var validationFilter = new IncomingRequestModelValidationActionFilter();
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("error", ErrorManager.ErrorCodes.INVALID_MINCOMP_VALUE);

            var actionContext = new ActionContext(
                new DefaultHttpContext()
                {
                    RequestServices = _serviceProvider.Object,
                },
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>(),
                modelState
            );

            var actionExecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                Mock.Of<Controller>()
            );

            validationFilter.OnActionExecuting(actionExecutingContext);
            Assert.AreEqual(StatusCodes.Status412PreconditionFailed, actionExecutingContext.Result.GetType()
                                        .GetProperty("StatusCode")
                                        .GetValue(actionExecutingContext.Result, null));
        }

        [TestMethod]
        public void ModelValidation_WhenAnInvalidErrorModel_ExceptionIsThrown()
        {
            var validationFilter = new IncomingRequestModelValidationActionFilter();
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("error", "Invalid_Code");

            var actionContext = new ActionContext(
                new DefaultHttpContext()
                {
                    RequestServices = _serviceProvider.Object,
                },
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>(),
                modelState
            );

            var actionExecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                Mock.Of<Controller>()
            );
            Assert.ThrowsException<NullReferenceException>(() => validationFilter.OnActionExecuting(actionExecutingContext));
        }
    }
}
