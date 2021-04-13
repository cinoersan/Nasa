namespace Nasa.Business.Extensions
{
    public static class StringExtensions
    {
        public static int? ToNullableInt(this string str)
        {
            return !int.TryParse(str, out var outData) ? (int?) null : outData;
        }
    }
}
