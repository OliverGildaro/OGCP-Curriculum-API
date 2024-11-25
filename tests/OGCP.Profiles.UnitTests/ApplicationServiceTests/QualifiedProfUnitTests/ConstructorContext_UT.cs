using ArtForAll.Shared.ErrorHandler;
using Moq;
using Moq.Language.Flow;
using OGCP.Curriculum.API.domainmodel;
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

            mockRepo
                .Setup(m => m.SaveChanges())
                .ReturnsAsync(() => 1);

            service = new QualifiedProfileService(mockRepo.Object);
        }

        public void Dispose()
        {
        }

        [Theory]
        [InlineData("Oliver", "Castro", "I am bla bla", "Fullstack software dev")]
        [InlineData("Carolina", "Castro", "I am bla bla", "Fullstack software dev2")]
        public async Task Test1(string firstName, string lastName, string summary, string rolePos)
        {
            var qualified = QualifiedProfile.Create(firstName, lastName, summary, rolePos).Value;
            var result = await service.Create(qualified);

            Assert.IsType<int>(result);
            Assert.Equal(1 , result);
        }
    }
}
