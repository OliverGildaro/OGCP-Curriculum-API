using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculums.Core.DomainModel.profiles;
using OGCP.Profiles.UnitTests.serviceTests.GeneralProfUnitTests;

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
        this.Add(StudentProfile.Create("Oliver", "Castro", "Summary here", "College", "To be that and this",
            PhoneNumber.CreateNew("591", "69554851").Value,
                "gildaro.castro@gmai.com").Value);
        this.Add(StudentProfile.Create("Carolina", "Castro", "Summary here", "University", "To be that and this",
            PhoneNumber.CreateNew("591", "69554851").Value,
                "gildaro.castro@gmai.com").Value);
    }
}

