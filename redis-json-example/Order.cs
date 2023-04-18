namespace RedisJsonExample;

class Order{

    public int OrderNumber {get; set;}
    public long OrderDateTime {get;set;}
    public string DeliveryAddress {get; set;}
    public double OrderTotal {get; set;}
    public bool Paid {get; set;}
    public bool Shipped {get; set;}
    public IEnumerable<OrderItem> Items {get; set;}

}

class OrderItem{
    public string Sku {get;set;}
    public string Description {get; set;}
    public int Quantity {get;set;}
    public double Price {get;set;}
}