using static Dagable.ErrorManagement.ErrorManager;

namespace Dagable.ErrorManagement
{
    public partial class DagableErrorManager : IDagableErrorManager
    {
        private readonly List<DagableError> Errors = new()
        {
            new DagableError(ErrorCodes.INTERNAL_SERVER_ERROR, "Something went wrong with your request."),
            new DagableError(ErrorCodes.INVALID_GRAPH_NAME, "A Graph name is required."),
            new DagableError(ErrorCodes.INVALID_TASK_GRAPH, "A valid task graph needs to be generated."),
            new DagableError(ErrorCodes.INVALID_PROCESSOR_COUNT, "The number of processors should be between 1 and 4."),
            new DagableError(ErrorCodes.INVALID_LAYERNODE_COUNT, "The number of nodes cannot be less then the number of layers."),
            new DagableError(ErrorCodes.INVALID_PERCENTAGE_COUNT, "The node connection percentage must be between 0 and 100."),
            new DagableError(ErrorCodes.INVALID_TASK_GRAPH, "The task graph that was submitted is not valid."),
            new DagableError(ErrorCodes.INVALID_NODE_COUNT, "The number of nodes is not valid."),
            new DagableError(ErrorCodes.INVALID_LAYER_COUNT, "The number of layers is not valid."),
            new DagableError(ErrorCodes.INVALID_MAXCOMM_VALUE, "The maximum communication time of is not valid."),
            new DagableError(ErrorCodes.INVALID_MINCOMM_VALUE, "The minimum communication time of is not valid."),
            new DagableError(ErrorCodes.INVALID_MINMAXCOMM_VALUE, "The minimum communication cannot be greater than the maximum communication time.."),
            new DagableError(ErrorCodes.INVALID_MAXCOMP_VALUE, "The maximum computation time of is not valid."),
            new DagableError(ErrorCodes.INVALID_MINCOMP_VALUE, "The minimum computation time of is not valid."),
            new DagableError(ErrorCodes.INVALID_MINMAXCOMP_VALUE, "The minimum computation time cannot be higher than the maximum computation time."),
            new DagableError(ErrorCodes.INVALID_NODE_SHAPE, "The node shape should either be box or circle."),
        };

        public DagableError this[string code]
        {
            get
            {
                var error = Errors.FirstOrDefault(x => x.Code == code);
                if (error == null)
                {
                    throw new NullReferenceException($"Error with code {code} could not be found");
                }
                return error;
            }
        }
    }
}