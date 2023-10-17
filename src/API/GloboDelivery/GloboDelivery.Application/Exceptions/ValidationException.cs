using FluentValidation.Results;

namespace GloboDelivery.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public Dictionary<string, string[]>? ValidationErrors { get; set; }
        public ValidationException() { }
        public ValidationException(string message) : base(message) { }
        public ValidationException(string message, Exception inner) : base(message, inner) { }
        public ValidationException(ValidationResult validationResult)
        {
            ValidationErrors = validationResult.Errors
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }
    }
}
