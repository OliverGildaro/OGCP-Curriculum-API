using ArtForAll.Shared.ErrorHandler;
using Moq;
using Moq.Language.Flow;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services;

namespace OGCP.Profiles.UnitTests.serviceTests.QualifiedProfUnitTests
{
    public class ConstructorContext_UT : IDisposable
    {
        private IReturnsResult<IQualifiedProfileRepository> repository;
        private QualifiedProfileService service;

        public ConstructorContext_UT()
        {
            var mockRepo = new Mock<IQualifiedProfileRepository>();
            mockRepo
                .Setup(m => m.Add(It.IsAny<QualifiedProfile>()))
                .Returns(Result.Success);

            service = new QualifiedProfileService(mockRepo.Object);
        }

        public void Dispose()
        {
        }

        [Theory]
        [InlineData("Oliver", "Castro", "I am bla bla", "Fullstack software dev")]
        [InlineData("Carolina", "Castro", "I am bla bla", "Fullstack software dev2")]
        public void Test1(string firstName, string lastName, string summary, string rolePos)
        {
            var qualified = new CreateQualifiedProfileRequest
            {
                FirstName = firstName,
                LastName = lastName,
                Summary = summary,
                DesiredJobRole = rolePos
            };
            var result = service.Create(qualified);

            Assert.IsType<Result>(result);
            Assert.NotNull(result);
            Assert.True(result.IsSucces);
            Assert.Empty(result.Message);
        }
    }
}
