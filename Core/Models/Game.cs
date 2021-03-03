using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    [Table("Game")]
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public string ReferenceName { get; set; }
        public string DisplayName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
