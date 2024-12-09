using ArtForAll.Shared.Contracts.DDD;

namespace OGCP.Curriculums.Core.DomainModel;

public static class Errors
{
    public static class Order
    {
        //public static Error TooManyEnrollments() =>
        //    new Error("student.too.many.enrollments", "Student cannot have more than 2 enrollments");

        //public static Error AlreadyEnrolled(string courseName) =>
        //    new Error("student.already.enrolled", $"Student is already enrolled into course '{courseName}'");

        //public static Error EmailIsTaken() =>
        //    new Error("user.email.is.taken", emailErrorMessages.emailTaken);

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

    public static class General
    {
        public static Error NotFound(string id = null)
        {
            string forId = id == null ? "" : $" for Id '{id}'";
            return new Error("record.not.found", $"Record not found{forId}");
        }

        public static Error ValueIsInvalid() =>
            new Error("value.is.invalid", "Value is invalid");

        public static Error ValueIsRequired() =>
            new Error("value.is.required", "Value is required");

        public static Error InvalidLength(string name = null)
        {
            string label = name == null ? " " : " " + name + " ";
            return new Error("invalid.string.length", $"Invalid{label}length");
        }

        public static Error CollectionIsTooSmall(int min, int current)
        {
            return new Error(
                "collection.is.too.small",
                $"The collection must contain {min} items or more. It contains {current} items.");
        }

        public static Error CollectionIsTooLarge(int max, int current)
        {
            return new Error(
                "collection.is.too.large",
                $"The collection must contain {max} items or more. It contains {current} items.");
        }

        public static Error InternalServerError(string message)
        {
            return new Error("internal.server.error", message);
        }
    }
}
