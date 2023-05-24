using Dagable.Api.Core.User;

namespace Dagable.Api.Core.Tests
{
    [TestClass]
    public class UserValidationTests
    {
        [TestMethod]
        [DataRow("box")]
        [DataRow("circle")]
        [DataRow("CircLe")]

        public void Validation_ValidUserSettings_ReturnsNoErrors(string shape)
        {
            UserSettingsDTO dto = new()
            {
                NodeShape = shape
            };

            var validationContext = new ValidationContext(dto);
            
            var result = dto.Validate(validationContext);

            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        [DataRow("square")]
        [DataRow("hexagon")]
        [DataRow("TriAngle")]

        public void Validation_ValidUserSettings_ReturnsInvalidShapeErrorError(string shape)
        {
            UserSettingsDTO dto = new()
            {
                NodeShape = shape
            };

            var validationContext = new ValidationContext(dto);

            var result = dto.Validate(validationContext);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(ErrorManagement.ErrorManager.ErrorCodes.INVALID_NODE_SHAPE, result.First().ErrorMessage);
        }
    }
}
