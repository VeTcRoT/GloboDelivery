using FluentValidation.Results;

namespace GloboDelivery.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public List<string>? ValidationErrors { get; set; }
        public ValidationException() { }
        public ValidationException(string message) : base(message) { }
        public ValidationException(string message, Exception inner) : base(message, inner) { }
        public ValidationException(ValidationResult validationResult)
        {
            ValidationErrors = new List<string>();

            foreach (var validationError in validationResult.Errors)
            {
                ValidationErrors.Add(validationError.ErrorMessage);
            }
        }
    }
}
