using OrdersTributoJustoTesteTecnico.Api.DependencyInjection;
using OrdersTributoJustoTesteTecnico.Ioc;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFiltersDependencyInjection();
builder.Services.AddOrdersTributoJustoTesteTecnicoDependencyInjection(configuration);
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
