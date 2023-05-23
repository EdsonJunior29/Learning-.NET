using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>();

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

public class User {
    public int Code { get; set; }
    public string Name { get; set; }
}

public static class UserRepository {
    public static List<User> Users {get; set;} = Users = new List<User>();

    //Obter configurações ao iniciar(Pegar as informações do arquivo appsettings)
    public static void Init(IConfiguration configuration){
        var users = configuration.GetSection("Users").Get<List<User>>();
        Users = users;
    }
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

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        =>optionsBuilder.UseSqlServer("Server=localhost;Database=Users;User Id=sa;Password=******;MultipleActiveResultSets=true;Encrypt=YES;TrustServerCertificate=YES");
}