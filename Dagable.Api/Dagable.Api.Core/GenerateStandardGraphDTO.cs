using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dagable.ErrorManagement;

namespace Dagable.Api.Core
{
    public class GenerateStandardGraphDTO : IValidatableObject
    {
        public int Nodes { get; set; }
        public int Layers { get; set; }
        public double Percentage { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Nodes <= 1)
            {
                yield return new ValidationResult(ErrorManager.ErrorCodes.INVALID_NODE_COUNT);
            }

            if (Layers <= 2)
            {
                yield return new ValidationResult(ErrorManager.ErrorCodes.INVALID_LAYER_COUNT);
            }

            if (Layers > Nodes)
            {
                yield return new ValidationResult(ErrorManager.ErrorCodes.INVALID_LAYERNODE_COUNT);
            }

            if (Percentage < 0 || Percentage > 100)
            {
                yield return new ValidationResult(ErrorManager.ErrorCodes.INVALID_PERCENTAGE_COUNT);
            }
        }
    }
}
