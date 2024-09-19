using AzureFunctionAppSample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos;
namespace AzureFunctionAppSample
{
    public  class ShoppingCartApi
    {
        private   readonly ILogger<ShoppingCartApi> _logger;
        private readonly CosmosClient _cosmosClient;

        public static   List<ShoppingCartItem> ShoppingCartItems = new List<ShoppingCartItem>();
        public ShoppingCartApi(ILogger<ShoppingCartApi> logger)
        {
            _logger = logger;
        }
        [Function("GetShoppingCartItems")]
        public  async Task< IActionResult> GetShoppingCartItems([HttpTrigger(AuthorizationLevel.Anonymous, "get", 
            Route = "shoppingcartitem")] HttpRequest req)
        {
            _logger.LogInformation("Getting all shopping cart items");
            return new OkObjectResult(ShoppingCartItems); 
        }

        [Function("GetShoppingCartItem")]
        public  async Task<IActionResult> GetShoppingCartItem([HttpTrigger(AuthorizationLevel.Anonymous, "get",  
            Route = "shoppingcartitem/{id}/{category}")] HttpRequest req, string id, string category)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var idString = req.RouteValues["id"]?.ToString();
            if (string.IsNullOrEmpty(id) || !Int32.TryParse(id, out int idPara))
            {
                return new BadRequestObjectResult("Please pass a valid id on the route");
            }
            var item = ShoppingCartItems.FirstOrDefault(x => x.Id == idPara);
            if (item == null)
            {
                return new NotFoundObjectResult("Item not found");
            }
            return new OkObjectResult(item);
        }
        [Function("CreateShoppingCartItem")]
        public    async Task<IActionResult> CreateShoppingCartItem([HttpTrigger(AuthorizationLevel.Anonymous, "post", 
            Route = "shoppingcartitem")] HttpRequest req)
        {
            _logger.LogInformation("creating shopping cart item.");
            var requestData= await new StreamReader(req.Body).ReadToEndAsync();
            var data= JsonConvert.DeserializeObject<CreateShoppingCartItem>(requestData);

            var item= new ShoppingCartItem
            {
                ItemName = data.ItemName,
                Category = "Grocery",
                Id = ShoppingCartItems.Count + 1,
                Collected = false,
               
            };
            ShoppingCartItems.Add(item);

            return new OkObjectResult(item);
        }

        [Function("PutShoppingCartItem")]
        public  async Task<IActionResult> PutShoppingCartItem([HttpTrigger(AuthorizationLevel.Anonymous, "put",
            Route = "shoppingcartitem/{id}/{category}")] HttpRequest req, string id, string category)
        {
            _logger.LogInformation("PutShoppingCartItem");

            var idString = req.RouteValues["id"]?.ToString();
            if (string.IsNullOrEmpty(id) || !Int32.TryParse(id, out int idPara))
            {
                return new BadRequestObjectResult("Please pass a valid id on the route");
            }
            var item = ShoppingCartItems.FirstOrDefault(x => x.Id == idPara);
            if (item == null)
            {
                return new NotFoundObjectResult("Item not found");
            }
            var requestData = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<UpdateShoppingCartItem>(requestData);
            item.ItemName = data.ItemName;
            item.Collected = data.Collected;
            return new OkObjectResult(item);

        }
        [Function("DeleteShoppingCartItem")]
        public  async Task<IActionResult> DeleteShoppingCartItem([HttpTrigger(AuthorizationLevel.Anonymous, "delete",
            Route = "shoppingcartitem/{id}/{category}")] HttpRequest req, string id, string category)
        {
            _logger.LogInformation("delete");

            var idString = req.RouteValues["id"]?.ToString();
            if (string.IsNullOrEmpty(id) || !Int32.TryParse(id, out int idPara))
            {
                return new BadRequestObjectResult("Please pass a valid id on the route");
            }
            var item = ShoppingCartItems.FirstOrDefault(x => x.Id == idPara);
            if (item == null)
            {
                return new NotFoundObjectResult("Item not found");
            }
            ShoppingCartItems.Remove(item);
            return new OkResult();
        }


    }
}
