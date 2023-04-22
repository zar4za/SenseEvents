using JwtService;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpLogging(options => options.LoggingFields = HttpLoggingFields.All);
builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection(AuthOptions.ConfigSection));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAny", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
    });
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAny");
app.UseAuthorization();
app.MapControllers();
app.Run();
