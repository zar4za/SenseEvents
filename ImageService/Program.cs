using ImageService;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

// ReSharper disable once RouteTemplates.RouteParameterIsNotPassedToMethod
app.MapGet("/api/images/{id:guid}", ([FromRoute] Guid id) => new ImageResponse()
{
    Exists = true
}).WithOpenApi()
    .Produces<ImageResponse>()
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
        Error = new ScError()
        {
            Message = "Unauthorized"
        }
    });
});

app.Run();
