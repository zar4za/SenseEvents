using AutoMapper;
using FluentValidation;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace SenseEvents.Infrastructure.Validation;

public class ValidationExceptionHandlingMiddleware : IMiddleware
{
    private readonly IMapper _mapper;

    public ValidationExceptionHandlingMiddleware(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e) when (e is ValidationException or ScException or InvalidOperationException)
        {
            context.Response.StatusCode = 400;
            var error = _mapper.Map<ScError>(e);
            var result = new ScResult(error);
            await context.Response.WriteAsJsonAsync(result);
        }
    }
}