using Dagable.ErrorManagement;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dagable.Api.Core
{
    public class GenerateCriticalGraphDTO : GenerateStandardGraphDTO, IValidatableObject
    {
        public int MaxComm { get; set; }
        public int MinComm { get; set; }
        public int MaxComp { get; set; }
        public int MinComp { get; set; }
        public int Processors { get; set; }

        public new IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MaxComm <= 0)
            {
                yield return new ValidationResult(ErrorManager.ErrorCodes.INVALID_MAXCOMM_VALUE);
            }

            if (MinComm <= 0)
            {
                yield return new ValidationResult(ErrorManager.ErrorCodes.INVALID_MINCOMM_VALUE);
            }
            if (MinComm > MaxComm)
            {
                yield return new ValidationResult(ErrorManager.ErrorCodes.INVALID_MINMAXCOMM_VALUE);
            }
            if (MaxComp <= 0)
            {
                yield return new ValidationResult(ErrorManager.ErrorCodes.INVALID_MAXCOMP_VALUE);
            }
            if (MinComp <= 0)
            {
                yield return new ValidationResult(ErrorManager.ErrorCodes.INVALID_MINCOMP_VALUE);
            }
            if (MinComp > MaxComp)
            {
                yield return new ValidationResult(ErrorManager.ErrorCodes.INVALID_MINMAXCOMP_VALUE);
            }
            if (Processors <= 0 || Processors > 4)
            {
                yield return new ValidationResult(ErrorManager.ErrorCodes.INVALID_PROCESSOR_COUNT);
            }
            
            var validationResults = base.Validate(validationContext);
            foreach (var result in validationResults)
            {
                yield return result;
            }
        }
    }
}
