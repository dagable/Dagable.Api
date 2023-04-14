using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dagable.Api.Core.Tests
{
    [TestClass]
    public class GenerateCriticalGraphTests
    {
        private GenerateCriticalGraphDTO _generateCriticalGraphDTO;

        [TestInitialize]
        public void TestIntialize()
        {
            _generateCriticalGraphDTO = new()
            {
                Layers = 10,
                Nodes = 30,
                Percentage = 0.50f
            };
        }

        [TestMethod]
        public void GenerateCriticalGraph_InvalidStandardGraphProperties_ReturnValidationErrors()
        {
            _generateCriticalGraphDTO.MaxComp = 100;
            _generateCriticalGraphDTO.MinComp = 10;
            _generateCriticalGraphDTO.MaxComm = 12;
            _generateCriticalGraphDTO.MinComm = 1;
            _generateCriticalGraphDTO.Processors = 3;
            _generateCriticalGraphDTO.Nodes = -10;
            _generateCriticalGraphDTO.Layers = -5;
            _generateCriticalGraphDTO.Percentage = -5;

            var validationContext = new ValidationContext(_generateCriticalGraphDTO);

            var result = _generateCriticalGraphDTO.Validate(validationContext);

            Assert.AreEqual(4, result.Count());
            Assert.AreEqual(ErrorManagement.ErrorManager.ErrorCodes.INVALID_NODE_COUNT, result.First().ErrorMessage);
            Assert.AreEqual(ErrorManagement.ErrorManager.ErrorCodes.INVALID_LAYER_COUNT, result.ElementAt(1).ErrorMessage);
            Assert.AreEqual(ErrorManagement.ErrorManager.ErrorCodes.INVALID_LAYERNODE_COUNT, result.ElementAt(2).ErrorMessage);
            Assert.AreEqual(ErrorManagement.ErrorManager.ErrorCodes.INVALID_PERCENTAGE_COUNT, result.Last().ErrorMessage);
        }

        [TestMethod]
        public void GenerateCriticalGraph_ValidProperties_NoErrorsReturns()
        {
            _generateCriticalGraphDTO.MaxComp = 100;
            _generateCriticalGraphDTO.MinComp = 10;
            _generateCriticalGraphDTO.MaxComm = 12;
            _generateCriticalGraphDTO.MinComm = 1;
            _generateCriticalGraphDTO.Processors = 3;
            var validationContext = new ValidationContext(_generateCriticalGraphDTO);

            var result = _generateCriticalGraphDTO.Validate(validationContext);

            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void GenerateCriticalPath_InvalidCompValues_ReturnsInvalidCompErrors()
        {
            _generateCriticalGraphDTO.MaxComp = -3;
            _generateCriticalGraphDTO.MinComp = -3;
            _generateCriticalGraphDTO.MaxComm = 12;
            _generateCriticalGraphDTO.MinComm = 1;
            _generateCriticalGraphDTO.Processors = 3;
            var validationContext = new ValidationContext(_generateCriticalGraphDTO);

            var result = _generateCriticalGraphDTO.Validate(validationContext);

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(ErrorManagement.ErrorManager.ErrorCodes.INVALID_MINCOMP_VALUE, result.Last().ErrorMessage);
            Assert.AreEqual(ErrorManagement.ErrorManager.ErrorCodes.INVALID_MAXCOMP_VALUE, result.First().ErrorMessage);
        }

        [TestMethod]
        public void GenerateCriticalPath_InvalidCommValues_ReturnsInvalidCommErrors()
        {
            _generateCriticalGraphDTO.MaxComp = 100;
            _generateCriticalGraphDTO.MinComp = 10;
            _generateCriticalGraphDTO.MaxComm = -2;
            _generateCriticalGraphDTO.MinComm = -8;
            _generateCriticalGraphDTO.Processors = 3;
            var validationContext = new ValidationContext(_generateCriticalGraphDTO);

            var result = _generateCriticalGraphDTO.Validate(validationContext);

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(ErrorManagement.ErrorManager.ErrorCodes.INVALID_MINCOMM_VALUE, result.Last().ErrorMessage);
            Assert.AreEqual(ErrorManagement.ErrorManager.ErrorCodes.INVALID_MAXCOMM_VALUE, result.First().ErrorMessage);
        }
    }
}
