using Microsoft.AspNetCore.Mvc.Filters;

namespace CursoAngular.API.Filters
{
    public class ExceptionLoggerFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionLoggerFilter> _logger;

        public ExceptionLoggerFilter(ILogger<ExceptionLoggerFilter> logger)
        {
            _logger = logger;
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);
            return base.OnExceptionAsync(context);
        }
    }
}
