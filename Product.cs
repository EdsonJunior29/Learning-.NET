public class Product {
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
     public int UserId { get; set; } //Informando que essa chave estrangerira e do tipo obrigatório
}
