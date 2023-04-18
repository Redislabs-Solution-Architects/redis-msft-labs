namespace RedisJsonExample;

using Microsoft.Extensions.Configuration;
using NRedisStack;
using NRedisStack.RedisStackCommands;
using StackExchange.Redis;
class Program
{
    static void Main(string[] args)
    {

        var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true);
        var config = builder.Build();
        var connectionString = config["REDIS_CONNECTION_STRING"];

        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(connectionString);
        IDatabase db = redis.GetDatabase();

        IJsonCommands json = db.JSON();

        var key = "order:6379";

        json.Set(key, "$", new Order{
            OrderNumber = 6379,
            OrderDateTime = 1682424000000,
            DeliveryAddress = "Microsoft UK, Thames Valley Park, Reading",
            OrderTotal = 0.00}
        );

        Console.WriteLine("Stored Order JSON. Press any key to continue...");
        Console.ReadLine();

        json.Set(key, "$.Items", new []{
            new OrderItem{Sku = "pizza001", Description = "margherita", Quantity = 4, Price = 8.50},
            new OrderItem{Sku = "pizza003", Description = "funghi", Quantity = 2, Price = 11.00},
            new OrderItem{Sku = "drinks008", Description = "camden hells lager", Quantity = 16, Price = 4.00}
        });

        var orderItemsLength = json.ArrLen(key, "$.Items").First();

        Console.WriteLine($"Added {orderItemsLength} items to Order JSON. Press any key to continue...");
        Console.ReadLine();

        json.ArrAppend(key, "$.Items", new OrderItem{Sku = "pizza002", Description = "prosciutto", Quantity = 2, Price = 13.00});

        orderItemsLength = json.ArrLen(key, "$.Items").First();

        Console.WriteLine($"Added another item to Order JSON - now {orderItemsLength} items. Press any key to continue...");
        Console.ReadLine();

        json.NumIncrby(key, "$.Items[2].Quantity", 8);

        json.Toggle(key, "$.Shipped");

    }
}
