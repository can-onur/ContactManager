namespace Report.Domain.Common.Guards
{
    public static class ObjectGuardExtensions
    {
        public static T GuardAgainstEmpty<T>(this T value, string message)
        {
            Guard.Against(() => IsEmpty(value), message);
            return value;
        }

        private static bool IsEmpty<T>(T value)
        {
            return value == null || value.Equals(default(T));
        }
    }
}
