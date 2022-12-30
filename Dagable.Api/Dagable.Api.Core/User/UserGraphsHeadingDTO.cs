using System;

namespace Dagable.Api.Core.User
{
    public class UserGraphsHeadingDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid GraphGuid { get; set; }
    }
}
