using System.Collections.Generic;

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
