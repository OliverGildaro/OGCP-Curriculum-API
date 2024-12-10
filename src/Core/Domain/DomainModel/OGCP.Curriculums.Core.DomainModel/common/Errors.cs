using System.Diagnostics.Metrics;

namespace OGCP.Curriculums.Core.DomainModel;

public static class Errors
{
    public static class Profiles
    {
        //public static Error TooManyEnrollments() =>
        //    new Error("student.too.many.enrollments", "Student cannot have more than 2 enrollments");

        //public static Error AlreadyEnrolled(string courseName) =>
        //    new Error("student.already.enrolled", $"Student is already enrolled into course '{courseName}'");

        public static Error EmailIsTaken() =>
            new Error("user.email.is.taken", "");

        public static Error InvalidEmail(string email) =>
             new Error("user.email.is.duplicated", "user email is duplicated");

        //public static Error InvalidFormatEmail(string email) =>
        //    new Error("user.email.is.wrong.Formatted", String.Format("{0}: {1}", emailErrorMessages.emailFormatError, email));

        public static Error InvalidCity(string name) =>
            new Error("invalid.city", $"Invalid city: '{name}'");

        public static Error InvalidOrderId(string orderId) =>
            new Error("invalid.orderId", $"Invalid orderId: '{orderId}'");

        public static Error PriceIsInvalid() =>
            new Error("price.is.invalid", "Price cannot be negatice or zero");
    }

    public class Educations
    {
        
    }

    public class Languages
    {
    }

    public static class Validation
    {
        
        public static Error ValueIsRequired(string fieldName) =>
            new Error("VALIDATION_REQUIRED_VALUE", $"Value is required for '{fieldName}'.");

        public static Error InvalidCountryCode(string fieldName) =>
            new Error("VALIDATION_REQUIRED_VALUE", $"{fieldName} must contain only digits.");

        public static Error InvalidPhoneNumber(string fieldName) =>
            new Error("VALIDATION_REQUIRED_VALUE", $"{fieldName} must contain only digits.");

        public static Error ValueIsInvalid() =>
            new Error("VALIDATION_INVALID_VALUE", "Value is invalid");

        public static Error InvalidLength(string name = null)
        {
            string label = name == null ? " " : " " + name + " ";
            return new Error("VALIDATION_INVALID_STRING_LENGTH", $"Invalid{label}length");
        }

        public static Error CollectionIsTooSmall(int min, int current)
        {
            return new Error(
                "VALIDATION_COLLECTION_TOO_SMALL",
                $"The collection must contain {min} items or more. It contains {current} items.");
        }

        public static Error CollectionIsTooLarge(int max, int current)
        {
            return new Error(
                "VALIDATION_COLLECTION_TOO_LARGE",
                $"The collection must contain {max} items or more. It contains {current} items.");
        }
    }

    public static class General
    {
        public static Error NotFound(int id) =>
            new Error("GENERAL_NOT_FOUND", $"Record not found for id: {id}");

        public static Error InternalServerError(string message) =>
            new Error("GENERAL_INTERNAL_SERVER_ERROR", message);

        public static Error EntityCanNotBeAddedTwice(string codeName, string entityName) =>
            new Error($"{codeName}_DUPLICATED_LANGUAGE", $"The same {entityName} can not be added twice");

        public static Error ValueIsRequired() =>
            new Error("VALIDATION_REQUIRED_VALUE", "Value is required");

    }
}
