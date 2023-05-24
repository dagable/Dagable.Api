using Dagable.Api.Core.Graph;
using Dagable.Core.Models;
using Dagable.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dagable.Api.Core.Tests
{
    [TestClass]
    public class RescheduleGraphTests
    {
        [TestMethod]
        public void RescheduleGraph_ValidProperties_NoErrorsReturned()
        {
            RescheduleGraphDTO dto = new()
            {
                Processors = 3,
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
        [DataRow(-4)]
        [DataRow(30)]
        public void RescheduleGraph_InvalidProcessors_InvalidProcessorsErrorsReturned(int processors)
        {
            RescheduleGraphDTO dto = new()
            {
                Processors = processors,
                TaskGraph = new TaskGraph.CriticalPath()
                {
                    dagGraph = new Graph<CriticalPathNode, CriticalPathEdge>()
                }
            };

            var validationContext = new ValidationContext(dto);

            var result = dto.Validate(validationContext);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(ErrorManagement.ErrorManager.ErrorCodes.INVALID_PROCESSOR_COUNT, result.First().ErrorMessage);
        }

        [TestMethod]
        public void RescheduleGraph_InvalidTaskGraph_InvaliTaskGraphErrorsReturned()
        {
            RescheduleGraphDTO dto = new()
            {
                Processors = 2,
            };

            var validationContext = new ValidationContext(dto);

            var result = dto.Validate(validationContext);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(ErrorManagement.ErrorManager.ErrorCodes.INVALID_TASK_GRAPH, result.First().ErrorMessage);
        }
    }
}
