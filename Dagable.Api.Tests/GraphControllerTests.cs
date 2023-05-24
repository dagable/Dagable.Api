using Dagable.Api.Controllers;
using Dagable.Api.Core.Graph;
using Dagable.Api.Services;
using Dagable.Api.Services.Graphs;
using Dagable.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Dagable.Api.Tests
{
    [TestClass]
    public class GraphControllerTests
    {
        private Mock<IDagScheduleServices> _dagScheduleServiceMock;
        private Mock<IDagGenerationServices> _dagGenerationServicesMock;
        private Mock<IGraphServices> _graphServicesMock;
        private Mock<ILogger<GraphsController>> _loggerMock;

        [TestInitialize]
        public void Initialize()
        {
            _dagScheduleServiceMock = new Mock<IDagScheduleServices>();
            _dagGenerationServicesMock = new Mock<IDagGenerationServices>();
            _graphServicesMock = new Mock<IGraphServices>();
            _loggerMock = new Mock<ILogger<GraphsController>>();
        }

        [TestMethod]
        public async Task Save_WhenValidGraph_ReturnsOk()
        {
            _graphServicesMock.Setup(x =>
               x.SaveGraph(It.IsAny<SaveGraphDTO>())
           ).ReturnsAsync(() => true);

            var graphController = new GraphsController(_loggerMock.Object, _dagGenerationServicesMock.Object,
                _dagScheduleServiceMock.Object, _graphServicesMock.Object);

            var result = await graphController.Save(new SaveGraphDTO());
            Assert.AreEqual(StatusCodes.Status200OK, ((IStatusCodeActionResult)result).StatusCode.Value);

        }

        [TestMethod]
        public async Task GetGraph_WhenInvalidGuid_ReturnsBadRequest()
        {
            var graphController = new GraphsController(_loggerMock.Object, _dagGenerationServicesMock.Object,
                _dagScheduleServiceMock.Object, _graphServicesMock.Object);

            var result = await graphController.GetGraph("Invalid Guid");
            Assert.AreEqual(StatusCodes.Status400BadRequest, ((IStatusCodeActionResult)result).StatusCode.Value);
        }

        [TestMethod]
        public async Task GetGraph_ValidGuid_ReturnsValidGraph()
        {
            _graphServicesMock.Setup(x =>
               x.GetGraphWithGuid(It.IsAny<Guid>())
           ).ReturnsAsync(() => new Mock<ICriticalPathTaskGraph>().Object);

            var graphController = new GraphsController(_loggerMock.Object, _dagGenerationServicesMock.Object,
                _dagScheduleServiceMock.Object, _graphServicesMock.Object);

            var result = await graphController.GetGraph(new Guid().ToString());
            Assert.AreEqual(StatusCodes.Status200OK, ((IStatusCodeActionResult)result).StatusCode.Value);
        }
    }
}