using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dagable.DataAccess.Migrations.DbModels
{
    [Table("User", Schema = "Dagable")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public UserSettings Settings { get; set; }     
        public virtual ICollection<Graph> Graphs { get; set;}
    }
}
