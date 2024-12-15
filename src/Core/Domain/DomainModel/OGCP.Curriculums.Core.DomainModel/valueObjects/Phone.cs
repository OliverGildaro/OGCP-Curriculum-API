using ArtForAll.Shared.Contracts.DDD;
using ArtForAll.Shared.ErrorHandler;
using System.Text.RegularExpressions;

namespace OGCP.Curriculums.Core.DomainModel.profiles;

public class PhoneNumber : ValueObject
{
    public string CountryCode { get; private set; }
    public string Number { get; private set; }

    public string FullNumber => $"+{CountryCode} {Number}";
    private static readonly Regex PhoneNumberRegex = new Regex(
        @"^\+(?<countryCode>\d{1,3})(?<number>\d+)$");

    protected PhoneNumber()
    {
    }

    private PhoneNumber(string countryCode, string number)
    {
        if (string.IsNullOrWhiteSpace(countryCode))
            throw new ArgumentException("Country code is required.", nameof(countryCode));

        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Phone number is required.", nameof(number));

        if (!Regex.IsMatch(countryCode, @"^\d+$"))
            throw new ArgumentException("Country code must contain only digits.", nameof(countryCode));

        if (!Regex.IsMatch(number, @"^\d+$"))
            throw new ArgumentException("Phone number must contain only digits.", nameof(number));

        this.CountryCode = countryCode;
        this.Number = number;
    }

    public static Result<PhoneNumber, Error> CreateNew(string countryCode, string number)
    {
        if (string.IsNullOrWhiteSpace(countryCode))
            return Errors.Validation.ValueIsRequired(nameof(countryCode));

        if (string.IsNullOrWhiteSpace(number))
            return Errors.Validation.ValueIsRequired(nameof(number));

        if (!Regex.IsMatch(countryCode, @"^\d+$"))
            return Errors.Validation.InvalidCountryCode(nameof(number));

        if (!Regex.IsMatch(number, @"^\d+$"))
            return Errors.Validation.InvalidPhoneNumber(nameof(number));

        return new PhoneNumber(countryCode, number);
    }

    public static PhoneNumber Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Phone number input cannot be empty.", nameof(input));

        var match = PhoneNumberRegex.Match(input);
        if (!match.Success)
            throw new FormatException("Invalid phone number format.");

        var countryCode = match.Groups["countryCode"].Value;
        var number = match.Groups["number"].Value;

        return new PhoneNumber(countryCode, number);
    }

    public static bool TryParse(string input, out PhoneNumber phoneNumber)
    {
        phoneNumber = null;

        if (string.IsNullOrWhiteSpace(input))
            return false;

        var match = PhoneNumberRegex.Match(input);
        if (!match.Success)
            return false;

        phoneNumber = new PhoneNumber(
            match.Groups["countryCode"].Value,
            match.Groups["number"].Value);

        return true;
    }

    public override string ToString() => FullNumber;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
