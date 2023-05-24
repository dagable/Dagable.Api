using Dagable.ErrorManagement;
using System.Collections.Generic;

namespace Dagable.Api.Models
{
    public class DagableStandardResult
    {
        public object? Data { get; set; }
        public bool IsError { get; set; }
        public IEnumerable<DagableError> Errors { get; set; }

        public DagableStandardResult(object response)
        {
            Data = response;
        }

        public DagableStandardResult(object response, bool isError, IEnumerable<DagableError> errors) : this(response)
        {
            IsError = isError;
            Errors = errors;
        }

        public DagableStandardResult(object response, bool isError, DagableError error) : this(response, isError, new List<DagableError> { error })
        {
        }
    }
}
