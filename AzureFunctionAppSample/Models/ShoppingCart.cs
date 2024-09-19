using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionAppSample.Models
{
    public class ShoppingCartItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public string ItemName { get; set; }    
        public bool Collected { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
    }
    public class CreateShoppingCartItem
    {
        public string ItemName { get; set; }
    }
    public class UpdateShoppingCartItem
    {
        public string ItemName { get; set; }
        public bool Collected { get; set; }
    }
}
