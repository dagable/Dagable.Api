using Dagable.Api.Pipeline.Filter;
using Dagable.ErrorManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace Dagable.Api.Tests
{
    [TestClass]
    public class DagableExceptionFilterTest
    {
        [TestMethod]
        public void ExceptionThrown_WhenExceptionThrown_500MaskedResponse()
        {
            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider
                .Setup(x => x.GetService(typeof(IDagableErrorManager)))
                .Returns(new DagableErrorManager());

            serviceProvider
                .Setup(x => x.GetService(typeof(ILogger<DagableExceptionFilter>)))
                .Returns(Mock.Of<ILogger<DagableExceptionFilter>>());

            var actionContext = new ActionContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    RequestServices = serviceProvider.Object,
                },
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor(),
            };

            var mockException = new Mock<Exception>();

            mockException.Setup(e => e.StackTrace)
              .Returns("Test stacktrace");
            mockException.Setup(e => e.Message)
              .Returns("Test message");
            mockException.Setup(e => e.Source)
              .Returns("Test source");

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = mockException.Object
            };

            var filter = new DagableExceptionFilter();

            filter.OnException(exceptionContext);
            Assert.AreEqual(500, exceptionContext.Result.GetType()
                                        .GetProperty("StatusCode")
                                        .GetValue(exceptionContext.Result, null));
        }
    }
}
