using System;

namespace Core.Models.Dtos
{
    public class GameInstanceStateDto
    {
        public DateTimeOffset DateCreated { get; set; }
        public string DataAsJson { get; set; }
    }
}
