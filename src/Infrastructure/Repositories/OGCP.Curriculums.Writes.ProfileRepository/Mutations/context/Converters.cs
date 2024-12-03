using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.DAL.Mutations.context;
public class EducationLevelConverter : ValueConverter<EducationLevel, string>
{
    public EducationLevelConverter()
        : base(
              v => v.ToString(),
              v => !string.IsNullOrWhiteSpace(v) ?
                (EducationLevel)Enum.Parse(typeof(EducationLevel), v)
                : EducationLevel.Bachelor)
    { }
}

public class ShortStringConverter : ValueConverter<string, string>
{
    public ShortStringConverter()
        : base(
              v => v,
              v => v,
              new ConverterMappingHints(size: 100))
    { }
}
