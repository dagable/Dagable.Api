using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dagable.DataAccess.Migrations.DbModels
{
    [Table("Edge", Schema = "Dagable")]
    public class Edge
    {
        [Key]
        public int Id { get; set; }
        public Graph Graph { get; set; }
        public double CommTime { get; set; }
        public int NodeFrom { get; set; }
        public int NodeTo { get; set; }
        [ForeignKey("NodeFrom")]
        public Node From { get; set; }
        [ForeignKey("NodeTo")]
        public Node To { get; set; }
        public bool IsCritical { get; set; }
    }
}
