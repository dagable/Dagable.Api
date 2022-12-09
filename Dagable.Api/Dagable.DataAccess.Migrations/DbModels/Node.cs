using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dagable.DataAccess.Migrations.DbModels
{
    [Table("Node", Schema = "Dagable")]
    public class Node
    {
        [Key]
        public int Id { get; set; }
        public int GraphId { get; set; }       
        public string Label { get; set; }
        public string IsCritical { get; set; }
        public double CompTime { get; set; }

        [ForeignKey("GraphId")]
        public Graph Graph { get; set; }
    }
}
