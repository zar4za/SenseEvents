using FluentValidation;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace SenseEvents.Infrastructure.Validation;

public class ValidationExceptionHandlingMiddleware : IMiddleware
{
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
        var scResult = new ScResult();

        switch (exception)
        {
            case ValidationException validationException:
                httpContext.Response.StatusCode = 400;
                scResult.Error = new ScError
                {
                    Message = exception.Message,
                    ModelState = validationException.Errors.ToDictionary(
                        x => x.PropertyName,
                        x => validationException.Errors
                            .Where(y => x.PropertyName == y.PropertyName)
                            .Select(y => y.ErrorMessage)
                            .ToList())
                };
                break;
            case ScException scException:
                httpContext.Response.StatusCode = 400;
                scResult.Error = new ScError
                {
                    Message = scException.Message
                };
                break;
            case InvalidOperationException invalidOperationException:
                httpContext.Response.StatusCode = 500;
                scResult.Error = new ScError
                {
                    Message = invalidOperationException.Message
                };
                break;
            default:
                throw exception;
        }

        await httpContext.Response.WriteAsJsonAsync(scResult);
    }
}