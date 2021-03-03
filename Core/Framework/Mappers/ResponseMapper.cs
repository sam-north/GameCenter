using Core.Framework.Models;

namespace Core.Framework.Mappers
{
    public static class ResponseMapper
    {
        public static Response<Target> Map<Source, Target>(Response<Source> source) where Source : class where Target : class
        {
            var target = new Response<Target>();
            target.Errors = source.Errors;
            target.Messages = source.Messages;
            return target;
        }
    }
}
