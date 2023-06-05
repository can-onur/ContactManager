using System.Runtime.Serialization;

namespace ContactManager.Domain.Exceptions
{
    [Serializable]
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }
    }
}