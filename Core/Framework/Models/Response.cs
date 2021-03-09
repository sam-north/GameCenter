using System.Collections.Generic;
using System.Linq;

namespace Core.Framework.Models
{
    public class Response<T> : IResponse<T> where T : class
    {
        public ICollection<string> Errors { get; set; } = new List<string>();
        public ICollection<string> Messages { get; set; } = new List<string>();
        public bool IsValid
        {
            get
            {
                return Errors == null || !Errors.Any();
            }
        }

        public T Data { get; set; }
    }
}
