namespace CursoAngular.API.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToDateTimeString(this DateTime? dateTime)
        {
            if (dateTime != null)
            {
                return dateTime.Value.ToShortDateString();
            }

            return string.Empty;
        }
    }
}
