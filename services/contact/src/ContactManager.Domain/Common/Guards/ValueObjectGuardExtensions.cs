namespace ContactManager.Domain.Common.Guards
{
    public static class ValueObjectGuardExtensions
    {
        public static IEnumerable<T> GuardAgainstDuplicate<T>(this IEnumerable<T> value, T item, string message)
        {
            Guard.Against(() => value.Contains(item), message);
            return value;
        }
    }
}
