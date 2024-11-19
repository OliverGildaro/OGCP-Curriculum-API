using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;

namespace OGCP.Curriculum.API.commanding
{
    public class Message
    {
        private readonly IServiceProvider provider;

        public Message(IServiceProvider provider)
        {
            this.provider = provider;
        }
        public Result DIspatch(ICommand command)
        {
            Type type = typeof(ICommandHandler<,>);
            Type[] args = { command.GetType(), typeof(Result) };
            Type genericType = type.MakeGenericType(args);
            
            dynamic handler = provider.GetService(genericType);
            return handler.HandleAsync((dynamic)command);
        }
    }
}
