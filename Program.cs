using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello Edson!");

app.MapPost("/user", (User user) =>
{
    UserRepository.Add(user);
});

app.MapGet("/user/{code}", ([FromRoute] int code) =>
{
    return UserRepository.GetByCode(code);
});

app.Run();

public class User {
    public int Code { get; set; }
    public string Name { get; set; }
}

public static class UserRepository {
    public static List<User> Users {get; set;}

    public static void Add(User user) {
        if (Users == null) {
            Users = new List<User>();
        }
        Users.Add(user);
    }

    public static User GetByCode(int code) {
        return Users.First(p => p.Code == code);
    }

}