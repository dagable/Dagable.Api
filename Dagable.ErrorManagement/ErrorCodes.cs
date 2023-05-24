namespace Dagable.ErrorManagement
{
    public partial class ErrorManager
    {
        public class ErrorCodes
        {
            private const string PREFIX = "DAG_ERR_";
            public const string BAD_REQUEST = PREFIX + "001";
            public const string INTERNAL_SERVER_ERROR = PREFIX + "002";
            public const string NULL_ARGUMENT = PREFIX + "003";
            public const string FORBIDDEN = PREFIX + "004";
            public const string PRECONDITIONS_FAILED = PREFIX + "005"; 
            public const string INVALID_JSON_DATA = PREFIX + "006";
            public const string INVALID_GRAPH_NAME = PREFIX + "007";
            public const string INVALID_TASK_GRAPH = PREFIX + "008";
            public const string INVALID_PROCESSOR_COUNT = PREFIX + "009";
            public const string INVALID_NODE_COUNT = PREFIX + "010";
            public const string INVALID_LAYER_COUNT = PREFIX + "011";
            public const string INVALID_LAYERNODE_COUNT = PREFIX + "012";
            public const string INVALID_PERCENTAGE_COUNT = PREFIX + "013";
            public const string INVALID_MAXCOMM_VALUE = PREFIX + "014";
            public const string INVALID_MINCOMM_VALUE = PREFIX + "015";
            public const string INVALID_MINMAXCOMM_VALUE = PREFIX + "016";
            public const string INVALID_MAXCOMP_VALUE = PREFIX + "017";
            public const string INVALID_MINCOMP_VALUE = PREFIX + "018";
            public const string INVALID_MINMAXCOMP_VALUE = PREFIX + "019";
            public const string INVALID_NODE_SHAPE = "020";
            public const string NOT_FOUND = "021";
        }
    }
}
