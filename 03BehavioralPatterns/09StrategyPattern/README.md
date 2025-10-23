# Strategy Pattern

Strategy Pattern, bir algoritmanın veya davranışın **çalışma zamanında (runtime)** dinamik olarak  
değiştirilebilmesini sağlayan behavioral pattern’dir.  

Bu pattern, farklı algoritmaların veya işlemlerin **ortak bir arayüz üzerinden yönetilmesini** sağlar.  
Yani “ne yapılacağı” sabittir ama “nasıl yapılacağı” seçilen stratejiye göre değişir.

## Gerçek Hayat Analojisi

Bir **ödeme sistemi** düşün 💳  
Müşteri ister kredi kartı, ister PayPal, ister kapıda ödeme seçebilir.  
Her ödeme yöntemi farklı şekilde çalışır ama hepsi aynı amaç için vardır: “ödeme yapmak”.  

Yani:
- **IPaymentStrategy** → ödeme stratejilerinin ortak arayüzü  
- **CreditCardPayment / PaypalPayment / CashOnDelivery** → farklı stratejiler  
- **Checkout** → context, yani hangi stratejinin uygulanacağını belirleyen sınıf  

## Program.cs (örnek kullanım)

```csharp
Console.WriteLine("Strategy Pattern - Payment Example...");

var orderAmount = 149.90m;

IPaymentStrategy creditCard = new CreditCardPayment("Taner", "1234-5678-9012-3456");
IPaymentStrategy paypal = new PaypalPayment("taner@example.com");
IPaymentStrategy cash = new CashOnDelivery();

var checkout = new Checkout();

// pay by credit card
checkout.SetStrategy(creditCard);
checkout.Pay(orderAmount);

// pay by PayPal
checkout.SetStrategy(paypal);
checkout.Pay(orderAmount);

// pay by cash on delivery
checkout.SetStrategy(cash);
checkout.Pay(orderAmount);

Console.ReadLine();

// ----- Strategy -----
interface IPaymentStrategy
{
    void Pay(decimal amount);
}

// ----- Concrete Strategies -----
class CreditCardPayment : IPaymentStrategy
{
    private readonly string _holder;
    private readonly string _cardNumber;

    public CreditCardPayment(string holder, string cardNumber)
    {
        _holder = holder;
        _cardNumber = cardNumber;
    }

    public void Pay(decimal amount)
    {
        Console.WriteLine($"💳 Charged {amount:C} to {_holder} (****{_cardNumber[^4..]})");
    }
}

class PaypalPayment : IPaymentStrategy
{
    private readonly string _email;
    public PaypalPayment(string email) => _email = email;

    public void Pay(decimal amount)
    {
        Console.WriteLine($"🅿️  Paid {amount:C} via PayPal ({_email})");
    }
}

class CashOnDelivery : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"💵 COD: {amount:C} will be collected on delivery");
    }
}

// ----- Context -----
class Checkout
{
    private IPaymentStrategy _strategy;

    public void SetStrategy(IPaymentStrategy strategy) => _strategy = strategy;

    public void Pay(decimal amount)
    {
        if (_strategy == null)
        {
            Console.WriteLine("No payment strategy selected.");
            return;
        }
        _strategy.Pay(amount);
    }
}
```

## Önemli Bilgi

Strategy Pattern genellikle şu durumlarda kullanılır:
- Bir işlevin birden fazla alternatif uygulaması varsa,  
- Farklı algoritmalar arasında **dinamik geçiş** yapmak gerekiyorsa,  
- Kodun if–else veya switch–case bloklarıyla dolmasını istemiyorsan.  

.NET dünyasında bu pattern:
- **Farklı ödeme yöntemleri**  
- **Farklı sıralama veya filtreleme algoritmaları**  
- **Loglama veya cache stratejileri**  
gibi senaryolarda sıkça kullanılır.  

✅ Yeni strateji eklemek kolaydır (var olan kodu değiştirmeden genişletme — *Open/Closed Principle*).  
✅ Kod okunabilirliği artar ve test edilebilirlik yükselir.  
