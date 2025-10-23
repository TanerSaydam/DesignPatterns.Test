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
