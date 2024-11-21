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
        public async Task<Result> DispatchCommand(ICommand command)
        {
            Type type = typeof(ICommandHandler<,>);
            Type[] args = { command.GetType(), typeof(Result) };
            Type genericType = type.MakeGenericType(args);
            
            dynamic handler = provider.GetService(genericType);
            return await handler.HandleAsync((dynamic)command);
        }

        public async Task<T> DispatchQuery<T>(IQuery<T> query)
        {
            Type typeHandler = typeof(IQueryHandler<,>);
            Type[] args = {query.GetType(), typeof(T) };
            Type genericType = typeHandler.MakeGenericType(args);

            dynamic handler = provider.GetService(genericType);

            T result = await handler.Handle((dynamic)query);
        
            return result;
        }
    }
}
