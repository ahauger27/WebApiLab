using System.Text.Json;
using WebApiLab.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

string jsonFile = File.ReadAllText(path: "./Resources/64KB.json");

var jsonData = JsonSerializer.Deserialize<List<Person>>(
    json: jsonFile, 
    options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true});

app.MapGet("/people", () => jsonData)
    .WithName("GetPeople")
    .Produces<List<Person>>(statusCode: StatusCodes.Status200OK);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

