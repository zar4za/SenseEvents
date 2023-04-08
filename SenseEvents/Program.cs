using FluentValidation;
using MediatR;
using SenseEvents.Features.Events;
using SenseEvents.Features.Tickets;
using SenseEvents.Infrastructure.Identity;
using SenseEvents.Infrastructure.Validation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
    
//singleton т.к. эти сервисы - заглушки, которые хранят состояние
builder.Services.AddSingleton<IGuidService, GuidService>();
builder.Services.AddSingleton<IEventsService, EventsServiceMock>();
builder.Services.AddTransient<IImageService, ImageServiceMock>();
builder.Services.AddTransient<ISpaceService, SpaceServiceMock>();
builder.Services.AddTransient<ITicketsService, TicketsServiceMock>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, ServiceLifetime.Transient);
builder.Services.AddTransient<ValidationExceptionHandlingMiddleware>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ValidationExceptionHandlingMiddleware>();
app.Run();
