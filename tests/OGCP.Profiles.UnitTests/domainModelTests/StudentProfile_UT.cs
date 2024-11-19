using OGCP.Curriculum.API.domainmodel;
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
        this.Add(StudentProfile.Create("Oliver", "Castro", "Summary here", "College", "To be that and this").Value);
        this.Add(StudentProfile.Create("Carolina", "Castro", "Summary here", "University", "To be that and this").Value);
    }
}

