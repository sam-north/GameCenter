using Core.Framework.Models;

namespace Core.Framework.Mappers
{
    public static class ResponseMapper
    {
        public static IResponse<Target> MapMetadata<Target>(IResponseMetadata source) where Target : class
        {
            var target = new Response<Target>();
            target.Errors = source.Errors;
            target.Messages = source.Messages;
            return target;
        }
    }
}
