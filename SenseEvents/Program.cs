using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SenseEvents.Features.Events;
using SenseEvents.Infrastructure.Identity;
using SenseEvents.Infrastructure.Validation;
using System.Reflection;
using System.Text;
using Polly;
using Polly.Extensions.Http;
using SenseEvents.Infrastructure.Mapping;
using SenseEvents.Infrastructure.Services;
using SenseEvents.Infrastructure.Services.Images;
using SenseEvents.Infrastructure.Services.Payments;
using SenseEvents.Infrastructure.Services.Spaces;

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
                      \r\n\r\nExample: 'Bearer 12345'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string> ()
        }
    });
});
builder.Services.Configure<ServiceOptions>(builder.Configuration.GetSection(ServiceOptions.ConfigSection));
builder.Services.Configure<EventsMongoOptions>(builder.Configuration.GetSection(EventsMongoOptions.ConfigSection));
builder.Services.AddSingleton<IGuidService, GuidService>();
builder.Services.AddSingleton<IEventsService, EventsService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAny", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
    });
});
builder.Services.AddHttpClient<IImageService, ImageHttpService>()
    .AddPolicyHandler(
        HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(10)));
builder.Services.AddHttpClient<ISpaceService, SpaceHttpService>()
    .AddPolicyHandler(
        HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(10)));
builder.Services.AddHttpClient<IPaymentsService, PaymentsHttpService>()
    .AddPolicyHandler(
        HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(10)));

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

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, ServiceLifetime.Transient);
builder.Services.AddTransient<ValidationExceptionHandlingMiddleware>();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAny");
app.UseMiddleware<ValidationExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
