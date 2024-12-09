namespace OGCP.Curriculums.API.Envelopes;

public class Envelope
{
    public object Result { get; }
    public List<EnvelopeChild> Errors { get; set; }
    public DateTime TimeGenerated { get; }

    private Envelope(object result, List<EnvelopeChild> errors)
    {
        Result = result;
        Errors = errors;
        TimeGenerated = DateTime.UtcNow;
    }

    public static Envelope Ok(object result = null)
    {
        return new Envelope(result, null);
    }

    internal static Envelope Error(List<EnvelopeChild> errors)
    {
        return new Envelope(null, errors);
    }
}
