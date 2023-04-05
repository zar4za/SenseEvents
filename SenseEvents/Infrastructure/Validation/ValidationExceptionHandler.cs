using FluentValidation;

namespace SenseEvents.Infrastructure.Validation
{
    public class ValidationExceptionHandlingMiddleware : IMiddleware
    {
        private const int ValidationExceptionStatusCode = 400;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            if (exception is not ValidationException validationException) throw exception;

            httpContext.Response.StatusCode = ValidationExceptionStatusCode;

            var response = new ErrorResponse()
            {
                Message = validationException.Message,
                Errors = validationException.Errors.Select(e => e.ToString())
            };

            await httpContext.Response.WriteAsJsonAsync(response);
        }
    }
}
