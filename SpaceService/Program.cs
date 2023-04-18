using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;
using SpaceService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseSwagger(); 
app.UseSwaggerUI();

// ReSharper disable once RouteTemplates.RouteParameterIsNotPassedToMethod
app.MapGet("/api/spaces/{id:guid}", ([FromRoute] Guid id) => new SpaceResponse
    {
        Exists = true
    }).WithOpenApi()
    .Produces<SpaceResponse>()
    .Produces<ScError>(401);

app.Use((context, next) =>
{
    // middleware заглушка для проверки на наличие токена аутентификации
    var containsToken = context.Request.Headers.Authorization.Any();

    if (containsToken)
        return next(context);

    context.Response.StatusCode = 401;
    context.Response.ContentType = "application/json";
    return context.Response.WriteAsJsonAsync(new ScResult
    {
        Error = new ScError
        {
            Message = "Unauthorized"
        }
    });
});

app.Run();