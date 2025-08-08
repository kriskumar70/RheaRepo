using ApiProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ApiProject.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Json Serialization: converting the data that we have fetched from the database using ef core to JSON format

//System.Text.Json is the default JSON serializer in .NET Core (built in)
//Microsoft.AspNetCore.Mvc.NewtonsoftJson is the package that we need to install to use Newtonsoft.Json as the JSON serializer

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{ 
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
}); //web api/restful api's

//config variable to access the appSettings.json file
var config = builder.Configuration;

//setting up IoC and DI for ef core in the project
//AddDbContext<> method is an extension method that is defined in the Microsoft.Extensions.DependencyInjection namespace
builder.Services.AddDbContext<ApiProjectDatabaseContext>(options => 
{
    // database provider and connection string
    options.UseSqlServer(config.GetConnectionString("ApiProjectCon"));
});

//setting up IoC and DI for repositories
//now the asp.net core IoC and DI has knowledge about our new repositories
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
