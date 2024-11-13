using JsonSubTypes;
using Newtonsoft.Json;
using OGCP.Curriculum.API.dtos;

public class ProfileRequestConverter : JsonConverter
{
    private JsonConverter _converter;
    public ProfileRequestConverter()
    {
        _converter = JsonSubtypesConverterBuilder
            .Of(typeof(ProfileRequest), "RequestType")
            .RegisterSubtype(typeof(CreateGeneralProfileRequest), ProfileEnum.CreateGeneralProfileRequest)
            .RegisterSubtype(typeof(CreateQualifiedProfileRequest), ProfileEnum.CreateQualifiedProfileRequest)
            .RegisterSubtype(typeof(CreateStudentProfileRequest), ProfileEnum.CreateStudentProfileRequest)
            .SerializeDiscriminatorProperty()
            .Build();
    }

    public override bool CanConvert(Type objectType)
    {
        return typeof(ProfileRequest).IsAssignableFrom(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        return _converter.ReadJson(reader, objectType, existingValue, serializer);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        _converter.WriteJson(writer, value, serializer);
    }
}
