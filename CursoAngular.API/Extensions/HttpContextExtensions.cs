using Microsoft.EntityFrameworkCore;

namespace CursoAngular.API.Extensions
{
    public static class HttpContextExtensions
    {
        public static void SetPaginationParameters(this HttpContext httpContext, int itemsCount)
        {
            httpContext?.Response.Headers.Add("itemsCount", itemsCount.ToString());
        }
    }
}
