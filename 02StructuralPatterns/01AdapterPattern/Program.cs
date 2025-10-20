Console.WriteLine("Adapter Pattern...");

INotificationSender emailSender = new EmailNotificationSender();
INotificationSender smsSender = new SmsAdapter(new SmsService());

emailSender.Send("user@example.com", "Hello via Email!");
smsSender.Send("+905551112233", "Hello via SMS!");

Console.ReadLine();

interface INotificationSender
{
    void Send(string to, string message);
}

class EmailNotificationSender : INotificationSender
{
    public void Send(string to, string message)
    {
        Console.WriteLine($"Email to {to}: {message}");
    }
}

class SmsService
{
    public void SendSms(string phoneNumber, string text)
    {
        Console.WriteLine($"SMS to {phoneNumber}: {text}");
    }
}

class SmsAdapter : INotificationSender
{
    private readonly SmsService _smsService;

    public SmsAdapter(SmsService smsService)
    {
        _smsService = smsService;
    }

    public void Send(string to, string message)
    {
        _smsService.SendSms(to, message);
    }
}