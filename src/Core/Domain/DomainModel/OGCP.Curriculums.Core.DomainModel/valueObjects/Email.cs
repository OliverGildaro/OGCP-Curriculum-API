namespace OGCP.Curriculums.Core.DomainModel.valueObjects;

using ArtForAll.Shared.Contracts.DDD;
using ArtForAll.Shared.ErrorHandler;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Email : ValueObject
{
    private readonly string _value;

    public string Value => _value;

    private static readonly Regex EmailRegex = new Regex(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be empty.", nameof(value));

        if (!EmailRegex.IsMatch(value))
            throw new ArgumentException("Invalid email format.", nameof(value));

        _value = value;
    }

    public static Result<Email, Error> CreateNew(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.Validation.ValueIsRequired(nameof(value));

        if (!EmailRegex.IsMatch(value))
            return Errors.Validation.InvalidEmail(nameof(value));

        return new Email(value);
    }

    public static Email Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Email input cannot be empty.", nameof(input));

        if (!EmailRegex.IsMatch(input))
            throw new FormatException("Invalid email format.");

        return new Email(input);
    }

    public static bool TryParse(string input, out Email email)
    {
        email = null;

        if (string.IsNullOrWhiteSpace(input))
            return false;

        if (!EmailRegex.IsMatch(input))
            return false;

        email = new Email(input);
        return true;
    }

    public override string ToString() => _value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value.ToLowerInvariant(); // Normalize case for comparison
    }
}

