using Core.Framework.Mappers;
using System.Collections.Generic;

namespace Core.Framework.Models
{
    public interface IResponse<T> : IResponseMetadata where T : class
    {
        T Data { get; set; }
    }
}
