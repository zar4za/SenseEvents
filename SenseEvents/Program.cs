using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SenseEvents.Features.Events;
using SenseEvents.Features.Tickets;
using SenseEvents.Infrastructure.Identity;
using SenseEvents.Infrastructure.Validation;
using System.Reflection;
using System.Text;

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
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
});
    
//singleton т.к. эти сервисы - заглушки, которые хранят состояние
builder.Services.AddSingleton<IGuidService, GuidService>();
builder.Services.AddSingleton<IEventsService, EventsServiceMock>();
builder.Services.AddTransient<IImageService, ImageServiceMock>();
builder.Services.AddTransient<ISpaceService, SpaceServiceMock>();
builder.Services.AddTransient<ITicketsService, TicketsServiceMock>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAny", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
    });
});

var authOptions = builder.Configuration.GetSection(AuthOptions.ConfigSection).Get<AuthOptions>()!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = authOptions.Authority,
            ValidAudience = authOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(authOptions.SecurityKey))
        };
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

app.UseCors("AllowAny");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ValidationExceptionHandlingMiddleware>();
app.Run();
