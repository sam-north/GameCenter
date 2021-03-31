using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.Views
{
    [Table("GameInstanceUserMessageView")]
    public class GameInstanceUserMessageViewResult
    {
        [Key]
        public long Id { get; set; }
        public Guid GameInstanceId { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string Text { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateDeleted { get; set; }
    }
}
