using Customers.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Customers.Application;
using Customers.Domain;
using Customers.Persistence;
using Customers.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.InstallApplicationLayer();
builder.Services.InstallDomainLayer();
builder.Services.InstallPersistenceLayer();

builder.Services.AddAutoMapper(config =>
{
    config.AddMaps(typeof(Program).Assembly);
    config.AddMaps(typeof(Customers.Application.Installer).Assembly);
    config.AddMaps(typeof(Customers.Persistence.Installer).Assembly);
});
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddDbContext<CustomerDbContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();
