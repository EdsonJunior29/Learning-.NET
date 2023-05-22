using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

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
        return Users.FirstOrDefault(p => p.Code == code);
    }

    public static void Delete(User user) {
        Users.Remove(user);
    }

}