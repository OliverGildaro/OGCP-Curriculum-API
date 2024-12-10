using ArtForAll.Shared.ErrorHandler;
using Moq;
using Moq.Language.Flow;
using OGCP.Curriculum.API.DAL.Mutations.Interfaces;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services;

namespace OGCP.Profiles.UnitTests.serviceTests.QualifiedProfUnitTests
{
    public class ConstructorContext_UT : IDisposable
    {
        private ProfileService service;

        public ConstructorContext_UT()
        {
            var mockRepo = new Mock<IProfileWriteRepo>();
            mockRepo
                .Setup(m => m.Add(It.IsAny<QualifiedProfile>()))
                .Returns(Result.Success);

            mockRepo
                .Setup(m => m.SaveChangesAsync())
                .ReturnsAsync(() => 1);

            service = new ProfileService(mockRepo.Object);
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
            var result = await service.CreateAsync(qualified);

            Assert.IsType<Result>(result);
            Assert.True(result.IsSucces);
        }
    }
}
