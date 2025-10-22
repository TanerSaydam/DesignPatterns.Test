Console.WriteLine("Decorator Pattern – Mail Service Example\n");

IMailService mail = new SmtpMailService();
mail = new LoggingMailDecorator(mail);
mail = new SignatureMailDecorator(mail, "\n--\nBest regards,\nACME Corp");

mail.Send("user@example.com", "Welcome", "Hello, welcome to our platform!");

Console.ReadLine();


public interface IMailService
{
    void Send(string to, string subject, string body);
}

public class SmtpMailService : IMailService
{
    public void Send(string to, string subject, string body)
    {
        Console.WriteLine($"Sending email to {to}\nSubject: {subject}\nBody:\n{body}\n");
    }
}

// Base decorator
public abstract class MailDecorator : IMailService
{
    protected readonly IMailService _inner;

    protected MailDecorator(IMailService inner)
    {
        _inner = inner;
    }

    public abstract void Send(string to, string subject, string body);
}

// Concrete decorators
public class LoggingMailDecorator : MailDecorator
{
    public LoggingMailDecorator(IMailService inner) : base(inner) { }

    public override void Send(string to, string subject, string body)
    {
        Console.WriteLine($"[LOG] Sending mail to {to} at {DateTime.Now}");
        _inner.Send(to, subject, body);
    }
}

public class SignatureMailDecorator : MailDecorator
{
    private readonly string _signature;
    public SignatureMailDecorator(IMailService inner, string signature) : base(inner)
    {
        _signature = signature;
    }

    public override void Send(string to, string subject, string body)
    {
        var signedBody = body + _signature;
        _inner.Send(to, subject, signedBody);
    }
}
