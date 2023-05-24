public class Category {
    public int Id { get; set; }
    public string Name { get; set; }
    public int UserId { get; set; } //Informando que essa chave estrangerira e do tipo obrigatório
    public User user { get; set; }
}
