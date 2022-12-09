using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dagable.DataAccess.Migrations.DbModels
{
    [Table("UserSettings", Schema = "Dagable")]
    public class UserSettings
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string NodeColor { get; set; }
        public string NodeStyle { get; set; }
        public bool IsVerticalLayout { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
