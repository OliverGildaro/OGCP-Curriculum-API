using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;

namespace OGCP.Curriculum.API.commanding.CreateQualifiedProfile
{
    public class CreateQualifiedProfileCommandHandler : ICommandHandler<CreateQualifiedProfileCommand, Result>
    {
        public Task<Result> HandleAsync(CreateQualifiedProfileCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
