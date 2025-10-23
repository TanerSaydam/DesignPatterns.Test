# Chain of Responsibility Pattern

Chain of Responsibility Pattern, bir isteğin birden fazla handler (işleyici) tarafından sırayla işlenmesini sağlayan bir behavioral pattern’dir.  
İstek zincir boyunca ilerler; her handler isteği işler veya bir sonrakine iletir.  
Böylece isteği kimin işleyeceğine karar verme sorumluluğu client’tan alınır.

## Gerçek Hayat Analojisi

Bir teknik destek hattı düşün ☎️  
1️⃣ Kullanıcı aradığında önce müşteri temsilcisine bağlanır.  
2️⃣ Sorun çözülmezse teknik destek birimine aktarılır.  
3️⃣ O da çözemezse yöneticilere iletilir.  

Her aşama isteği kontrol eder;  
çözebilirse kendisi halleder, çözemezse zincirdeki bir sonraki kişiye aktarır.  
**Chain of Responsibility Pattern**, bu tür hiyerarşik sorumluluk aktarımlarını düzenler.

## Program.cs (örnek kullanım)

```csharp
Console.WriteLine("Chain of Responsibility Pattern...");

#region Exampla 1
var low = new Level1Support();
var mid = new Level2Support();
var high = new ManagerSupport();

// chain connections
low.SetNext(mid).SetNext(high);

// send requests
low.HandleRequest(new SupportRequest("Password reset", 1));
low.HandleRequest(new SupportRequest("Email not syncing", 2));
low.HandleRequest(new SupportRequest("System outage", 3));
low.HandleRequest(new SupportRequest("Database corruption", 4));
#endregion

#region Example 2
var steps = new List<string> { "CustomerRep", "Production", "Boss" }; // can be changed dynamically
var approvals = new ApprovalChain(steps);

var order = new Order(101, 25000, "Laptop Order");
await approvals.StartApproval(order);
#endregion

Console.ReadLine();

#region Example 1
// Request model
class SupportRequest
{
    public string Message { get; }
    public int Level { get; }

    public SupportRequest(string message, int level)
    {
        Message = message;
        Level = level;
    }
}

// Handler interface
abstract class SupportHandler
{
    protected SupportHandler _next;

    public SupportHandler SetNext(SupportHandler next)
    {
        _next = next;
        return next;
    }

    public abstract void HandleRequest(SupportRequest request);
}

// Concrete Handlers
class Level1Support : SupportHandler
{
    public override void HandleRequest(SupportRequest request)
    {
        if (request.Level == 1)
            Console.WriteLine($"[Level 1] Resolved: {request.Message}");
        else
            _next?.HandleRequest(request);
    }
}

class Level2Support : SupportHandler
{
    public override void HandleRequest(SupportRequest request)
    {
        if (request.Level == 2)
            Console.WriteLine($"[Level 2] Resolved: {request.Message}");
        else
            _next?.HandleRequest(request);
    }
}

class ManagerSupport : SupportHandler
{
    public override void HandleRequest(SupportRequest request)
    {
        if (request.Level >= 3)
            Console.WriteLine($"[Manager] Escalated issue handled: {request.Message}");
        else
            Console.WriteLine($"[Manager] Ignored request: {request.Message}");
    }
}
#endregion

#region Example 2
record Order(int Id, decimal Amount, string Description);

class ApprovalChain
{
    private readonly List<string> _steps;

    public ApprovalChain(List<string> steps)
    {
        _steps = steps;
    }

    public async Task StartApproval(Order order)
    {
        Console.WriteLine($"Order #{order.Id} Approval Process Started...\n");

        foreach (var step in _steps)
        {
            Console.WriteLine($"-- {step} is reviewing order...");
            bool approved = await ApproveAsync(step, order);

            if (!approved)
            {
                Console.WriteLine($"{step} rejected the order. Process stopped.\n");
                return;
            }

            Console.WriteLine($"{step} approved the order.\n");
        }

        Console.WriteLine($"All approvals completed. Order #{order.Id} has been created successfully!\n");
    }

    private static Task<bool> ApproveAsync(string step, Order order)
    {
        // Dummy logic: You can replace this with real validation rules per step
        return Task.FromResult(true);
    }
}
#endregion
```
