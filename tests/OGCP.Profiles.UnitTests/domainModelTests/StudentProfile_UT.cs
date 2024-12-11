using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculums.Core.DomainModel.profiles;
using OGCP.Curriculums.Core.DomainModel.valueObjects;

namespace OGCP.Profiles.UnitTests.domainModelTests;

public class StudentProfile_UT
{
    [Theory]
    [ClassData(typeof(QualifiedProfileClassData))]
    public void Test1(StudentProfile student)
    {

    }
}


public class QualifiedProfileClassData : TheoryData<StudentProfile>
{
    public QualifiedProfileClassData()
    {
        this.Add(StudentProfile.Create(
            Name.CreateNew("Oliver", "Castro").Value,
            "Summary here",
            "College",
            "To be that and this",
            PhoneNumber.CreateNew("591", "69554851").Value,
            Email.CreateNew("gildaro.castro@gmai.com").Value).Value);
    }
}

