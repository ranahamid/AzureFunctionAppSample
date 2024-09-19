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
        
        public string Id { get; set; } = Guid.NewGuid().ToString();
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
