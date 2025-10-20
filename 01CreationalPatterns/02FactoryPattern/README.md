# Factory Pattern

Factory Pattern, object olu�turma s�recini merkezi bir noktada toplar.  
Bu sayede object olu�turmak i�in `new` anahtar s�zc���n� do�rudan kullanmadan, **factory** arac�l���yla objectleri dinamik olarak olu�turabiliriz.  
Ama�, object olu�turmay� soyutlamak ve kod ba��ml�l���n� azaltmakt�r.

## Ne ��e Yarar?

- Nesne olu�turma s�recini tek bir yerde toplar.  
- Kodun, hangi class�n olu�turulaca��n� bilmesine gerek kalmaz.  
- Yeni t�rler eklendi�inde mevcut kod de�i�tirilmeden geni�letilebilir.

> **Not:** Factory Pattern, SOLID prensiplerinden **Open�Closed Principle** (A��k�Kapal� �lkesi) ile uyumludur.  
> Yeni bir notification t�r� eklemek i�in mevcut kodu de�i�tirmeden yeni bir class olu�turmak yeterlidir.  
> B�ylece kod **geni�letmeye a��k**, **de�i�ikli�e kapal�** olur.


## Kod �rne�i

A�a��da `NotificationFactory`, `EmailNotification` ve `SmsNotification` classlar�yla �rnek bir kullan�m g�sterilmi�tir.

```csharp
Console.WriteLine("Factory Pattern...");

INotification emailNotification = NotificationFactory.CreateNotification(NotificationTypeEnum.Email);
emailNotification.Send("Hello world");

INotification smsNotification = NotificationFactory.CreateNotification(NotificationTypeEnum.Sms);
smsNotification.Send("Hello world");

Console.ReadLine();

interface INotification
{
    void Send(string message);
}

class EmailNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine("Email sent: {0}", message);
    }
}

class SmsNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine("Sms sent: {0}", message);
    }
}

class NotificationFactory
{
    private NotificationFactory() { }
    public static INotification CreateNotification(NotificationTypeEnum notificationType)
    {
        switch (notificationType)
        {
            case NotificationTypeEnum.Email:
                return new EmailNotification();
            case NotificationTypeEnum.Sms:
                return new SmsNotification();
            default:
                throw new ArgumentException("Invalid notification type");
        }
    }
}

enum NotificationTypeEnum
{
    Email,
    Sms
}
```

## Ger�ek Hayatta Kullan�m�

- Bildirim sistemlerinde (Email, SMS, Push Notification)
- Farkl� �deme sistemlerinde (Stripe, PayPal, Iyzico)