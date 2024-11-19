using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Profiles.UnitTests.domainModelTests;

public class Profile_UT
{
    [Theory]
    [InlineData(null, "Castro", "I am a fullstack java dev with bla")]
    public void CreateQualifiedProfile_fails(string firstName, string lastName, string summary)
    {
        var result = Assert.Throws<ArgumentNullException>(
        () => new Profile(firstName, lastName, summary));

        Assert.Equal("Value cannot be null. (Parameter 'firstName')", result.Message);
    }
}
