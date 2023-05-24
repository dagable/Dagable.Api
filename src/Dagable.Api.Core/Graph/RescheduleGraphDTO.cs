using Dagable.Core;
using Dagable.ErrorManagement;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dagable.Api.Core.Graph
{
    public class RescheduleGraphDTO : IValidatableObject
    {
        public int Processors { get; set; }
        public ICriticalPathTaskGraph TaskGraph { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TaskGraph == null)
            {
                yield return new ValidationResult(ErrorManager.ErrorCodes.INVALID_TASK_GRAPH);
            }
            if (Processors <= 0 || Processors > 4)
            {
                yield return new ValidationResult(ErrorManager.ErrorCodes.INVALID_PROCESSOR_COUNT);
            }
        }
    }
}
