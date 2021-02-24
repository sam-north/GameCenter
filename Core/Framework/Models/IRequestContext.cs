namespace Core.Framework.Models
{
    public interface IRequestContext
    {
        public int UserId { get; set; }
        public string Email { get; set; }
    }
}
