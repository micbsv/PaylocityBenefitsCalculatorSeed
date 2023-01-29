using Api;
using Api.PaycheckCalculator;
using Api.Repository;
using Api.Services;
using Api.Validators;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Dependency Injection: Add services to the container
builder.Services.AddScoped<IEmployeesService, EmployeesService>();
builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddScoped<IValidator, AddEmployeeValidator>();
builder.Services.AddScoped<IPaycheckCalculator, PaycheckCalculator>();
builder.Services.AddScoped<IPaycheckService, PaycheckService>();
builder.Services.AddScoped<IDependentsService, DependentsService>();
builder.Services.AddScoped<IDependentsRepository, DependentsRepository>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Employee Benefit Cost Calculation Api",
        Description = "Api to support employee benefit cost calculations"
    });
});

var allowLocalhost = "allow localhost";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowLocalhost,
        policy  =>
        {
            policy.WithOrigins("http://localhost:3000", "http://localhost");
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowLocalhost);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
