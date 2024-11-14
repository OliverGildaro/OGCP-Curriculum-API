namespace OGCP.Curriculum.API.domainModel
{
    public enum typeProfile
    {
        QUALIFIED_PROFILE = 1,
        GENERAL_PROFILE
    }

    public enum DegreeEnum
    {
        AssociateDegree,
        BachelorDegree,
        MasterDegree,
        Doctorate,
        PhD,
        Diploma,
        Certificate,
        HighSchool
    }



    public enum LevelEnum
    {
        BEGINNER = 1,
        INTERMEDIATE = 2,
        PROFICIENT = 3,
        ADVANCED = 4,
        EXPERT = 5
    }



    public enum LanguageEnum
    {
        SPANISH = 1,
        ENGLISH = 2,
        KOREAN = 3,
        ITALIAN = 4,
        PORTUGUESE = 5
    }


    public enum WorkExperiences
    {
        WORK = 1,
        INTERSHIP
    }

    public enum ProfileEnum
    {
        CreateGeneralProfileRequest = 1,
        CreateQualifiedProfileRequest,
        CreateStudentProfileRequest
    }

}

