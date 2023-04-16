using Microsoft.AspNetCore.Mvc;
using PaymentsService.AddPayment;
using SC.Internship.Common.ScResult;
using AutoMapper;
using PaymentsService;
using PaymentsService.ChangeState;
using SC.Internship.Common.Exceptions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();
app.UseSwagger(); 
app.UseSwaggerUI();

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
    // middleware �������� ��� �������� �� ������� ������ ��������������
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
