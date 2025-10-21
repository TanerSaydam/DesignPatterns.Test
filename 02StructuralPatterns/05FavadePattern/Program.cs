Console.WriteLine("Facade Pattern...");

var shop = new ShopFacade();
shop.PlaceOrder("iPhone 19", "Taner", "1234-4567-7895-0152", "Kayseri");

Console.ReadLine();

class InventoryService
{
    public bool IsAvailable(string product)
    {
        Console.WriteLine($"[Inventory] Checking availablity for '{product}'...");
        return true; // Assume all products are in stock
    }
}

class PaymentService
{
    public bool ProcessPayment(string customer, string cardNumber)
    {
        Console.WriteLine($"[Payment] Processing payment for {customer} with card {cardNumber}...");
        return true; // Simulate successful payment;
    }
}

class DeliveryService
{
    public void ScheduleDelivery(string product, string address)
    {
        Console.WriteLine($"[Delivery] Scheduling delivery of '{product}' to {address}...");
    }
}

class ShopFacade
{
    private readonly InventoryService _inventory = new();
    private readonly PaymentService _payment = new();
    private readonly DeliveryService _delivery = new();

    public void PlaceOrder(string product, string customer, string cardNumber, string address)
    {
        Console.WriteLine("\n--- Procesing Order ---");

        if (!_inventory.IsAvailable(product))
        {
            Console.WriteLine($"Product '{product}' is out of stock!");
            return;
        }

        if (!_payment.ProcessPayment(customer, cardNumber))
        {
            Console.WriteLine("Payment failed!");
            return;
        }

        _delivery.ScheduleDelivery(product, address);
        Console.WriteLine("Order completed successfully! \n");
    }
}