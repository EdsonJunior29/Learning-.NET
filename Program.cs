using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["Database:SqlServer"]);

var app = builder.Build();
var configuration = app.Configuration;
UserRepository.Init(configuration);

app.MapGet("/", () => "Hello Edson Junior!");

app.MapPost("/user", (User user) =>
{
    UserRepository.Add(user);
    return Results.Created("/user/" + user.Code, user.Code);
});

app.MapGet("/user/{code}", ([FromRoute] int code) =>
{
    var user = UserRepository.GetByCode(code);
    
    if(user == null) {
        return Results.NotFound();
    }

    return Results.Ok(user);
});

app.MapPut("/user", (User user) => {
    var userSaved = UserRepository.GetByCode(user.Code);
    
    if(userSaved == null) {
        return Results.NotFound();
    }

    userSaved.Name = user.Name;

    return Results.NoContent();
});

app.MapDelete("/user/{code}", ([FromRoute] int code) => {
    var userSaved = UserRepository.GetByCode(code);
    UserRepository.Delete(userSaved);

    return Results.NoContent();
});

app.MapGet("/configuration/database", (IConfiguration configuration) => {
    return Results.Ok($"{configuration["database:connection"]}/{configuration["database:port"]}");
});

app.Run();
