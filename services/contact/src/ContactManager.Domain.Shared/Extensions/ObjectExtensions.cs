namespace ContactManager.Domain.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsEmpty<T>(this T value)
        {
            return value == null || value.Equals(default(T));
        }
    }
}
