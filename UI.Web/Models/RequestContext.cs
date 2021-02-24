using Core.Framework.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace UI.Web.Models
{
    public class RequestContext : IRequestContext
    {
        public RequestContext(IHttpContextAccessor httpContextAccessor)
        {
            Email = httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(x => x.Type == nameof(Email))?.Value;
            int userId;
            int.TryParse(httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(x => x.Type == nameof(UserId))?.Value, out userId);
            UserId = userId;
        }
        public int UserId { get; set; }
        public string Email { get; set; }
    }
}
