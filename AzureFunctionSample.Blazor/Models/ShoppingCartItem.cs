namespace AzureFunctionSample.Blazor.Models
{
    public class ShoppingCartItem
    {
       
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public string ItemName { get; set; }
        public bool Collected { get; set; }
       
        public string Category { get; set; }
    }
    public class CreateShoppingCartItem
    {
        public string ItemName { get; set; }
        public string Category { get; set; }
    }
    public class UpdateShoppingCartItem
    {
        public string ItemName { get; set; }
        public bool Collected { get; set; }
    }
}
