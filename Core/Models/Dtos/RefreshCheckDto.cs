using System;

namespace Core.Models.Dtos
{
    public class RefreshCheckDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
