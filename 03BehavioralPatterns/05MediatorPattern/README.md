# Mediator Pattern

Mediator Pattern, bir sistemdeki class’ların birbirleriyle **doğrudan iletişim kurmak yerine**,  
bir **aracı (mediator)** üzerinden haberleşmesini sağlayan behavioral pattern’dir.  

Bu yaklaşım, class’lar arasındaki bağımlılığı azaltır ve sistemi daha esnek hale getirir.

## Gerçek Hayat Analojisi

Bir **sohbet odası (ChatRoom)** düşün 💬  
- Kullanıcılar (class’lar) **birbirlerine doğrudan mesaj göndermez**.  
- Her kullanıcı yalnızca **ChatRoom (Mediator)** ile konuşur: `Send(to, message)` çağrısını ChatRoom’a yapar.  
- **ChatRoom**, kime gideceğine karar verip mesajı **doğru kullanıcıya yönlendirir** (`Receive(from, message)`).

Yani:
- **Users** = bağımsız component’ler (colleague’ler)  
- **ChatRoom (Mediator)** = iletişim yöneticisi; kayıt (`Register`), yönlendirme (`SendMessage`) ve kuralları merkezî yönetir

Fayda: Kullanıcılar birbirinin referansını bilmez; ekleme/çıkarma kolaydır, bağımlılık azalır ve iletişim mantığı tek yerde toplanır.

## Program.cs (örnek kullanım)

```csharp
Console.WriteLine("Mediator Pattern");

var chatRoom = new ChatRoomMediator();

var user1 = new User("Taner", chatRoom);
var user2 = new User("Ayşe", chatRoom);
var user3 = new User("Mehmet", chatRoom);

chatRoom.Register(user1);
chatRoom.Register(user2);
chatRoom.Register(user3);

user1.Send("Ayşe", "Merhaba Ayşe!");
user2.Send("Mehmet", "Selam Mehmet, nasılsın?");
user3.Send("Taner", "Taner abi proje bitti!");

Console.ReadLine();

// ----- Mediator interface -----
interface IChatMediator
{
    void Register(User user);
    void SendMessage(string from, string to, string message);
}

// ----- Concrete Mediator -----
class ChatRoomMediator : IChatMediator
{
    private readonly Dictionary<string, User> _users = new();

    public void Register(User user)
    {
        if (!_users.ContainsKey(user.Name))
        {
            _users[user.Name] = user;
        }
    }

    public void SendMessage(string from, string to, string message)
    {
        if (_users.TryGetValue(to, out var receiver))
        {
            receiver.Receive(from, message);
        }
        else
        {
            Console.WriteLine($"User '{to}' not found in chat room.");
        }
    }
}

// ----- Colleague -----
class User
{
    public string Name { get; }
    private readonly IChatMediator _mediator;

    public User(string name, IChatMediator mediator)
    {
        Name = name;
        _mediator = mediator;
    }

    public void Send(string to, string message)
    {
        Console.WriteLine($"{Name} -> {to}: {message}");
        _mediator.SendMessage(Name, to, message);
    }

    public void Receive(string from, string message)
    {
        Console.WriteLine($"{Name} received from {from}: {message}");
    }
}

```

## Modern Yaklaşım (Günümüzdeki Kullanımı)

Mediator Pattern günümüzde hâlâ çok aktif biçimde kullanılır,  
ancak genellikle daha gelişmiş yapılarla entegre edilmiştir:

- **MediatR (CQRS yapısında)** → Command/Query + Handler modelini uygular.  
  Ayrıca “Pipeline Behavior” kavramıyla loglama, validation, caching gibi işleri merkezîleştirir.  
- **Event Bus / Event Aggregator** → component’ler doğrudan haberleşmek yerine publish/subscribe modeliyle konuşur.  
- **Message Broker sistemleri** (ör. RabbitMQ, Azure Service Bus, Kafka) → sistemler arası mediator görevi görür.  
- **Domain Events (DDD)** → aggregate içinde değişiklikleri event olarak yayıp, ilgili handler’ların dinlemesini sağlar.  
- **UI tarafında:**  
  - WPF / WinUI: `ICommand` ve Messenger yapıları mediator görevi görür.  
  - Angular / React / Blazor: merkezi store (Redux, NGXS, Zustand) mediator işlevi görür.  
- **ASP.NET Core Pipeline:** middleware ve filter zinciriyle isteklerin akışını merkezden yönetir.  

Bu modern yapılarda **Mediator fikri** aynıdır, ancak “tek dev aracı” yerine  
**sorumluluğu paylaşan, olay tabanlı (event-driven)** yapılarla uygulanır. ✅
