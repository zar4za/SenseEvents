using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;
using AutoMapper;
using Microsoft.AspNetCore.HttpLogging;
using PaymentsService;
using PaymentsService.Shared;
using PaymentsService.Shared.AddPayment;
using SC.Internship.Common.Exceptions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpLogging(options => options.LoggingFields = HttpLoggingFields.All);
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();
app.UseSwagger(); 
app.UseSwaggerUI();
app.UseHttpLogging();

var payments = new List<Payment>();

app.MapPost("api/payments", ([FromBody] AddPaymentCommand command, IMapper mapper) =>
{
    var payment = mapper.Map<Payment>(command);
    payments.Add(payment);
    return payment;
});

app.MapPut("api/payments/{id:guid}/confirm", ([FromRoute] Guid id) =>
{
    var payment = payments.First(p => p.Id == id);

    if (payment.State != PaymentState.Hold)
        throw new ScException("Payment was confirmed or cancelled.");

    payment.State = PaymentState.Confirmed;
    payment.DateConfirmation = DateTimeOffset.Now;

    return payment;
});

app.MapPut("api/payments/{id:guid}/cancel", ([FromRoute] Guid id) =>
{
    var payment = payments.First(p => p.Id == id);

    if (payment.State != PaymentState.Hold)
        throw new ScException("Payment was confirmed or cancelled.");

    payment.State = PaymentState.Canceled;
    payment.DateCancellation = DateTimeOffset.Now;

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
        Error = new ScError
        {
            Message = "Unauthorized"
        }
    });
});

app.Run();
