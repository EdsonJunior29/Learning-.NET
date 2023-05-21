using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello Edson!");

app.MapPost("/user", (User user) =>
{
    return user.Code + " - " + user.Name;
});

app.MapGet("/getuser", ([FromQuery] string code , [FromQuery] string name) =>
{
    // Requisição via postman (http://localhost:5124/getuser?code=1&name=Edson)
    return code + " - " + name;
});


app.MapGet("/getuser2/{code}/{name}", ([FromRoute] string code, [FromRoute] string name) =>
{
    // Requisição via postman (http://localhost:5124/getuser2/1/EdsonJunior)
    return code + " - " + name;
});

app.Run();

public class User {
    public int Code { get; set; }
    public string Name { get; set; }
}