using Dagable.Api.Core.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dagable.Api.Core.Tests
{
    [TestClass]
    public class GenerateStandardGraphTests
    {
        [TestMethod]
        public void GenerateStandardGraph_ValidProperties_ReturnsNoErrors()
        {
            GenerateStandardGraphDTO dto = new()
            {
                Layers = 10,
                Nodes = 20,
                Percentage = 0.50f
            };

            var validationContext = new ValidationContext(dto);

            var result = dto.Validate(validationContext);

            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void GenerateStandardGraph_TooFewNodes_ReturnsInvalidNodeLayerErrors()
        {
            GenerateStandardGraphDTO dto = new()
            {
                Layers = 10,
                Nodes = -2,
                Percentage = 0.50f
            };

            var validationContext = new ValidationContext(dto);

            var result = dto.Validate(validationContext);

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(ErrorManagement.ErrorManager.ErrorCodes.INVALID_NODE_COUNT, result.First().ErrorMessage);
            Assert.AreEqual(ErrorManagement.ErrorManager.ErrorCodes.INVALID_LAYERNODE_COUNT, result.Last().ErrorMessage);
        }


        [TestMethod]
        public void GenerateStandardGraph_TooFewLayers_ReturnsInvalidLayerCountErrors()
        {
            GenerateStandardGraphDTO dto = new()
            {
                Layers = -1,
                Nodes = 30,
                Percentage = 0.50f
            };

            var validationContext = new ValidationContext(dto);

            var result = dto.Validate(validationContext);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(ErrorManagement.ErrorManager.ErrorCodes.INVALID_LAYER_COUNT, result.First().ErrorMessage);
        }

        [TestMethod]
        [DataRow(-5f)]
        [DataRow(1000f)]
        public void GenerateStandardGraph_InvalidPercenage_ReturnsInvalidPercentageError(double percentage)
        {
            GenerateStandardGraphDTO dto = new()
            {
                Layers = 20,
                Nodes = 30,
                Percentage = percentage
            };

            var validationContext = new ValidationContext(dto);

            var result = dto.Validate(validationContext);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(ErrorManagement.ErrorManager.ErrorCodes.INVALID_PERCENTAGE_COUNT, result.First().ErrorMessage);
        }
    }
}
