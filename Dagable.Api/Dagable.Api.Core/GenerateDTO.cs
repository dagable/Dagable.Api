using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dagable.Api.RequestModels
{
    public class GenerateGraphDTO
    {
        public int Nodes { get; set; }
        public int Layers { get; set; }
        public double Percentage { get; set; }
    }
}
