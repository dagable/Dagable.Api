using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dagable.DataAccess.Migrations.DbModels
{
    [Table("Graph", Schema = "Dagable")]
    public class Graph
    {
        [Key]
        public int Id { get; set; }
        public Guid GraphGuid { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [InverseProperty("Graph")]
        public virtual ICollection<Node> Nodes { get; set; }

        [InverseProperty("Graph")]
        public virtual ICollection<Edge> Edges { get; set; }
    }
}
