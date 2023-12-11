using CustomerManagementApi.Repositories;
using CustomerManagementApi.Services;
using CustomerManagementApi;
using Microsoft.Extensions.Configuration;
using FluentValidation;
using System;
using CustomerManagementApi.Validators;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using CustomerManagementApi.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string? environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var configuration = new ConfigurationBuilder()
               .AddJsonFile($"appsettings.{environmentName}.json")
               .Build();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
var serilog = new LoggerConfiguration()
               .ReadFrom.Configuration(configuration)
               .CreateLogger();
builder.Services.AddSerilog(serilog);

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService,CustomerService>();
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CustomerValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureExceptionHandler();    //Injected generic middle ware
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
