using System;

namespace Core.Models.Dtos
{
    public class GameInstanceUserMessageDto
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string Text { get; set; }
        public DateTimeOffset DateCreated { get; set; }
    }
}
