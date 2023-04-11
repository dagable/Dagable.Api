using Dagable.Core;
using Dagable.ErrorManagement;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dagable.Api.Core.Graph
{
    public class SaveGraphDTO : IValidatableObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICriticalPathTaskGraph TaskGraph { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Name))
            {
                yield return new ValidationResult(ErrorManager.ErrorCodes.INVALID_GRAPH_NAME);
            }
            if (TaskGraph == null)
            {
                yield return new ValidationResult(ErrorManager.ErrorCodes.INVALID_TASK_GRAPH);
            }
        }
    }
}
