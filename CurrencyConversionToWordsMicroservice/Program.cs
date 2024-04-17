using CurrencyConversionToWordsMicroservice.Handler;
using CurrencyConversionToWordsMicroservice.Middlewares;
using CurrencyConversionToWordsMicroservice.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<INumberToWordConverterHandler, NumberToWordConverterHandler>();
builder.Services.AddControllers();
builder.Services.AddScoped<IValidator<double>, CurrencyValidator>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyHeader().WithOrigins("http://localhost:4200"));
app.MapControllers();

app.Run();
