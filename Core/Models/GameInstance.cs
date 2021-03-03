using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    [Table("GameInstance")]
    public class GameInstance
    {
        [Key]
        public Guid Id { get; set; }
        public int GameId { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public bool IsDeleted { get; set; }

        public GameInstanceState State { get; set; }
        public ICollection<GameInstanceUser> Users { get; set; } = new List<GameInstanceUser>();
    }
}
