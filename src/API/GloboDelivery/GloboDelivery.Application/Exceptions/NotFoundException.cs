namespace GloboDelivery.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, Exception inner) : base(message, inner) { }
        public NotFoundException(string name, object key)
           : base($"{name} ({key}) is not found") { }
    }
}
