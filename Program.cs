var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello Edson!");

app.MapPost("/user", (User user) =>
{
    return user.Code + " - " + user.Name;
});
app.Run();


public class User {
    public int Code { get; set; }
    public string Name { get; set; }
}