using ArtForAll.Shared.Contracts.DDD;
using ArtForAll.Shared.ErrorHandler;
using System.Text.RegularExpressions;

namespace OGCP.Curriculums.Core.DomainModel.valueObjects;
public class Name : ValueObject
{
    protected string _givenName;
    protected string _familyNames;

    public string GivenName => _givenName;
    public string FamilyNames => _familyNames;

    public string FullName => string.IsNullOrEmpty(FamilyNames)
        ? GivenName
        : $"{GivenName} {FamilyNames}";

    private static readonly Regex regex = new Regex(@"^[\p{L}\p{M}'\- ]+$");

    private Name(string givenName, string familyNames)
    {
        if (!string.IsNullOrWhiteSpace(givenName) && !regex.IsMatch(familyNames))
            throw new ArgumentException("Given name name contains invalid characters.", nameof(givenName));

        if (!string.IsNullOrWhiteSpace(familyNames) && !regex.IsMatch(familyNames))
            throw new ArgumentException("Family names contains invalid characters.", nameof(familyNames));

        _givenName = givenName.Trim();
        _familyNames = familyNames?.Trim();
    }

    public static Result<Name, Error> CreateNew(string givenName, string familyNames)
    {
        if (string.IsNullOrWhiteSpace(givenName))
            return Errors.Validation.ValueIsRequired(nameof(givenName));

        if (!Regex.IsMatch(givenName, @"^[\p{L}\p{M}'\- ]+$"))
            return Errors.Validation.ValueIsRequired(nameof(givenName));

        if (!string.IsNullOrWhiteSpace(familyNames) && !regex.IsMatch(familyNames))
        {
            return Errors.Validation.ValueIsRequired(nameof(familyNames));
        }

        return new Name(givenName, familyNames);
    }

    public override string ToString() => FullName;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _givenName.ToLowerInvariant();
        yield return _familyNames?.ToLowerInvariant() ?? string.Empty;
    }
}
