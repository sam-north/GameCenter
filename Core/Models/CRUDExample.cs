using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    [Table("CRUDExample")]
    public class CRUDExample
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DateDeleted { get; set; }
    }
}