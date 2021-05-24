using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Validation.Domain
{
    public sealed class Error : ValueObject
    {
        private const string Separator = "||";
        internal Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; }
        public string Message { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }

        public string Serialize()
        {
            return $"{Code}{Separator}{Message}";
        }

        public static Error Deserialize(string serialized)
        {
            if (serialized == "A non-empty request body is required.")
                return Errors.General.ValueIsRequired();

            var data = serialized.Split(new[] {Separator}, StringSplitOptions.RemoveEmptyEntries);
            if (data.Length < 2)
                throw new Exception($"Invalid error serialization: '{serialized}'");

            return new Error(data[0], data[1]);
        }
    }

    public static class Errors
    {
        public static class General
        {
            public static Error NotFound(long? id)
            {
                var forId = id == null ? string.Empty : $" for Id '{id}'";
                return new Error("record.not.found", $"Record not found {forId}");
            }
            public static Error ValueIsInvalid() =>
                new("value.is.invalid", "Value is invalid");

            public static Error ValueIsRequired() =>
                new("value.is.required", "Value is required");

            public static Error InvalidLength(string name = null)
            {
                var label = name ?? string.Empty;
                return new Error("invalid.string.length", $"Invalid {label} length");
            }

            public static Error CollectionsIsTooSmall(int min, int current)
            {
                return new("collection.is.too.small",
                    $"The collection must contains more than {min} items. Currently: {current}");
            }

            public static Error CollectionsIsToLarge(int max, int current)
            {
                return new("collection.is.too.large",
                    $"The collection must contains less than {max} items. Currently: {current}");
            }

            public static Error InternalServerError(string message)
            {
                return new("internal.server.error", message);
            }
        }

        public static class Student
        {
            public static Error TooManyEnrollments() =>
                new Error("student.too.many.enrollments", "Student cannot have more than 2 enrollments");

            public static Error AlreadyEnrolled(string courseName) =>
                new Error("student.already.enrolled", $"Student is already enrolled into course '{courseName}'");

            public static Error EmailIsTaken()
            {
                return new("email.is.taken", "Email is taken");
            }

            public static Error CourseIsInvalid() =>
                new Error("course.is.invalid", "Course is invalid");
        }
    }
}