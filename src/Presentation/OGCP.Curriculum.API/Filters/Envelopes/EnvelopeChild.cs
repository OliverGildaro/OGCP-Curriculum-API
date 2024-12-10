using ArtForAll.Shared.Contracts.DDD;
using OGCP.Curriculum.API.Filters.Envelopes;
using OGCP.Curriculums.Core.DomainModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OGCP.Curriculums.API.Envelopes;

public class EnvelopeChild
{
    public string Code { get; }
    public string ErrorMessage { get; }
    public string InvalidField { get; }

    private EnvelopeChild(object result, KeyValueError error)
    {
        Code = error?.Error.Code;
        ErrorMessage = error?.Error.Message;
        InvalidField = error?.FieldName;
    }

    public static EnvelopeChild Ok(object result = null)
    {
        return new EnvelopeChild(result, null);
    }

    public static List<EnvelopeChild> Error(List<KeyValueError> errors)
    {
        return errors.Select(e => new EnvelopeChild(null, e)).ToList();
    }
}
