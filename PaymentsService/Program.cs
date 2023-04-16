using Microsoft.AspNetCore.Mvc;
using PaymentsService.AddPayment;
using SC.Internship.Common.ScResult;
using AutoMapper;
using PaymentsService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();
app.UseSwagger(); 
app.UseSwaggerUI();

app.MapPost("api/payments", ([FromBody] AddPaymentCommand command, IMapper mapper) =>
{
    var payment = mapper.Map<Payment>(command);

    return payment;
});

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
