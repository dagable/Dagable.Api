using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dagable.Api.Core.User
{
    public class UserSettingsDTO : IValidatableObject
    {
        public string NodeColor { get; set; } = "#ffff";
        public string NodeShape { get; set; } = "circle";
        public bool UseVerticalLayout { get; set; } = true;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NodeShape.ToLower() != "circle" && NodeShape.ToLower() != "box")
            {
                yield return new ValidationResult(ErrorManagement.ErrorManager.ErrorCodes.INVALID_NODE_SHAPE);
            }
        }
    }
}
