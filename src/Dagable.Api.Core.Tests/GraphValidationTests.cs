using Dagable.Api.Core.Graph;
using Dagable.Core;
using Dagable.Core.Models;

namespace Dagable.Api.Core.Tests
{
    [TestClass]
    public class GraphValidationTests
    {
        [TestMethod]
        public void Validation_ValidDtoObject_ReturnsNoErrors()
        {
            SaveGraphDTO dto = new()
            {
                Name = "name",
                TaskGraph = new TaskGraph.CriticalPath()
                {
                    dagGraph = new Graph<CriticalPathNode, CriticalPathEdge>()
                }
            };

            var validationContext = new ValidationContext(dto);

            var result = dto.Validate(validationContext);

            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void Validation_NullOrEmptyName_ReturnsInvalidGraphNameError()
        {
            SaveGraphDTO dto = new()
            {
                TaskGraph = new TaskGraph.CriticalPath()
                {
                    dagGraph = new Graph<CriticalPathNode, CriticalPathEdge>()
                }
            };

            var validationContext = new ValidationContext(dto);

            var result = dto.Validate(validationContext);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(ErrorManagement.ErrorManager.ErrorCodes.INVALID_GRAPH_NAME, result.First().ErrorMessage);
        }

        [TestMethod]
        public void Validation_NullTaskGraph_ReturnsInvalidTaskGraph()
        {
            SaveGraphDTO dto = new()
            {
                Name = "test",
            };

            var validationContext = new ValidationContext(dto);

            var result = dto.Validate(validationContext);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(ErrorManagement.ErrorManager.ErrorCodes.INVALID_TASK_GRAPH, result.First().ErrorMessage);
        }

        [TestMethod]
        public void Validation_NullTaskGraphAndNoName_ReturnsInvalidTaskGraphAndInvalidName()
        {
            SaveGraphDTO dto = new();

            var validationContext = new ValidationContext(dto);

            var result = dto.Validate(validationContext);

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(ErrorManagement.ErrorManager.ErrorCodes.INVALID_GRAPH_NAME, result.First().ErrorMessage);
            Assert.AreEqual(ErrorManagement.ErrorManager.ErrorCodes.INVALID_TASK_GRAPH, result.Last().ErrorMessage);
        }
    }
}