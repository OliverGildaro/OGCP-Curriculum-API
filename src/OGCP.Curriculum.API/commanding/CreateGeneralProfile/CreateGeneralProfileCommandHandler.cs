using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;

namespace OGCP.Curriculum.API.commanding.CreateGeneralProfile
{
    public class CreateGeneralProfileCommandHandler : ICommandHandler<CreateGeneralProfileCommand, Result>
    {
        public Task<Result> HandleAsync(CreateGeneralProfileCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
