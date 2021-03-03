using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    [Table("GameInstanceUser")]
    public class GameInstanceUser
    {
        [Key]
        public long Id { get; set; }
        public Guid GameInstanceId { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; }
    }
}
