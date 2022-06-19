using Dagable.Core;
using System.Collections.Generic;

namespace Dagable.Api.Services
{
    public interface IDagServices
    {
        string CreateDag();
    }

    public class DagServices : IDagServices
    {
        public string CreateDag()
        {
            var dagGraph = new DagCreator().Generate();
            return dagGraph.AsJson();
        }
    }
}
