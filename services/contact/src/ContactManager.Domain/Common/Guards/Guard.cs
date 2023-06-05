using ContactManager.Domain.Exceptions;

namespace ContactManager.Domain.Common.Guards
{
    public static class Guard
    {
        public static void Against(this Func<bool> func, string message)
        {
            if (func())
            {
                throw new ValidationException(message);
            }
        }
    }
}
