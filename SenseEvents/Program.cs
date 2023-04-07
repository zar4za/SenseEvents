using FluentValidation;
using MediatR;
using SenseEvents.Features.Events;
using SenseEvents.Features.Id;
using SenseEvents.Infrastructure.Validation;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});
builder.Services.AddSwaggerGen();

//singleton т.к. эти сервисы - заглушки, которые хранят состояние
builder.Services.AddSingleton<IGuidService, GuidService>();
builder.Services.AddSingleton<IEventsService, EventsServiceMock>();
builder.Services.AddTransient<IImageService, ImageServiceMock>();
builder.Services.AddTransient<ISpaceService, SpaceServiceMock>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, ServiceLifetime.Transient);
builder.Services.AddTransient<ValidationExceptionHandlingMiddleware>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ValidationExceptionHandlingMiddleware>();
app.Run();
