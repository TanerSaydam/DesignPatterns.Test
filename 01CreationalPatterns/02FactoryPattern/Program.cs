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