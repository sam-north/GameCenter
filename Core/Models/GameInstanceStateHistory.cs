using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    [Table("GameInstanceStateHistory")]
    public class GameInstanceStateHistory
    {
        [Key]
        public long Id { get; set; }
        public Guid GameInstanceId { get; set; }
        public string DataAsJson { get; set; }
        public string Message { get; set; }
        public DateTimeOffset DateCreated { get; set; }
    }
}
