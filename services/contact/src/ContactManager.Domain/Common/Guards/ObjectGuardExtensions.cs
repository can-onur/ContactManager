using ContactManager.Domain.Shared.Extensions;

namespace ContactManager.Domain.Common.Guards
{
    public static class ObjectGuardExtensions
    {
        public static T GuardAgainstEmpty<T>(this T value, string message)
        {
            Guard.Against(() => value.IsEmpty(), message);
            return value;
        }
    }
}
