using System.Collections.Generic;

namespace Core.Framework.Models
{
    public interface IResponseMetadata
    {
        ICollection<string> Errors { get; set; }
        ICollection<string> Messages { get; set; }
        bool IsValid { get; }
    }
}
