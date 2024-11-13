namespace OGCP.Curriculum.API.Handlers
{
    public interface ICommandHandler<TCommand, TResult>
    {
        public TResult Handle(TCommand command);
    }
}
