using System;
using System.Collections.Generic;

namespace Core.Models.Dtos
{
    public class UserGameInstanceStatelessDto
    {
        public Guid Id { get; set; }
        public int GameId { get; set; }
        public string GameDisplayName { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public ICollection<GameInstanceUserDto> Users { get; set; } = new List<GameInstanceUserDto>();
    }
}
