using Newtonsoft.Json.Linq;
using OGCP.Curriculum.API.dtos;

namespace OGCP.Curriculum.API.helpers;
 public class ProfileConverter : JsonCreationConverter<ProfileRequest>
{
    protected override ProfileRequest Create(Type objectType, JObject jObject)
    {
        if (jObject == null) throw new ArgumentNullException(nameof(jObject));
        if (jObject["SchoolName"] != null)
            return new CreateGeneralProfileRequest();
        else if (jObject["HospitalName"] != null)
            return new CreateQualifiedProfileRequest();
        else
            return new CreateStudentProfileRequest();
    }
}