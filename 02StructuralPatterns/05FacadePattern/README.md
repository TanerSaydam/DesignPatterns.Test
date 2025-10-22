# Facade Pattern

Facade Pattern, karmaşık bir sistemi basitleştirmek için kullanılan bir structural pattern’dir.  
Birden fazla alt sistemin (subsystem) karmaşık yapısını client’tan gizleyerek,  
tek bir arayüz (facade) üzerinden bu sistemlerle kolayca etkileşim kurmayı sağlar.

## Amaç

- Karmaşık sistemleri basit bir arayüz arkasında toplamak.  
- Client’ın alt sistemlerin detaylarını bilmeden işlemleri gerçekleştirmesini sağlamak.  
- Kodun okunabilirliğini artırmak ve bağımlılıkları azaltmak.

## Gerçek Hayat Analojisi

Bir alışveriş mağazasını aradığında sipariş vermek için bir operatörle konuştuğunu düşün ☎️  
Operatör, mağazanın stok kontrolü, ödeme sistemleri ve teslimat servisleri gibi farklı departmanlarına erişimi yönetir.  
Sen sadece operatörle konuşursun; o, arkadaki karmaşık sistemi senin yerine koordine eder.  
**Facade Pattern**, bu operatör gibi davranarak sistemi basitleştirir ve kullanıcıya tek bir giriş noktası sunar.

## Program.cs (örnek kullanım)

```csharp
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
```
