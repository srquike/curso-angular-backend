namespace CursoAngular.API.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToDateString(this DateTime? dateTime)
        {
            if (dateTime != null)
            {
                return dateTime.Value.ToString("MMMM dd, yyyy");
            }

            return string.Empty;
        }
    }
}
