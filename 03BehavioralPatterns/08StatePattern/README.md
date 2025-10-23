# State Pattern

State Pattern, bir object’in iç durumuna (state) göre davranışının değişmesini sağlayan behavioral pattern’dir.  
Bu pattern sayesinde, if–else veya switch bloklarıyla karmaşık state yönetimi yapmak yerine  
her durumu (state) ayrı bir class olarak tanımlayıp yönetebilirsin.

## Gerçek Hayat Analojisi

Bir **müzik çalar (Audio Player)** düşün 🎧  
- Oynatma durumu **Playing**,  
- Duraklatılmış durumu **Paused**,  
- Durdurulmuş durumu **Stopped** olabilir.  

Kullanıcı “Play/Pause/Stop” tuşuna bastığında cihazın davranışı o anki duruma göre değişir.  
Yani:
- **AudioPlayer** = Context  
- **IPlayerState** = State arayüzü  
- **Playing / Paused / Stopped** = Concrete State class’ları  

Her state, diğer state’e nasıl geçileceğini ve hangi eylemleri desteklediğini kendi içinde tanımlar.

## Program.cs (örnek kullanım)

```csharp
Console.WriteLine("State Pattern - Audio Player Example...");

var player = new AudioPlayer();

player.PlayPause(); // Stopped -> Playing
player.PlayPause(); // Playing -> Paused
player.Stop();      // Paused -> Stopped
player.Stop();      // Stopped -> (no-op)
player.PlayPause(); // Stopped -> Playing
player.Stop();      // Playing -> Stopped

Console.ReadLine();

// Context
class AudioPlayer
{
    private IPlayerState _state = new StoppedState();

    public void SetState(IPlayerState state)
    {
        _state = state;
        Console.WriteLine($"[State] → {state.Name}");
    }

    public void PlayPause() => _state.PlayPause(this);
    public void Stop() => _state.Stop(this);
}

// State
interface IPlayerState
{
    string Name { get; }
    void PlayPause(AudioPlayer player);
    void Stop(AudioPlayer player);
}

// Concrete States
class StoppedState : IPlayerState
{
    public string Name => "Stopped";

    public void PlayPause(AudioPlayer player)
    {
        Console.WriteLine("▶️ Start playing");
        player.SetState(new PlayingState());
    }

    public void Stop(AudioPlayer player)
    {
        Console.WriteLine("⏹ Already stopped");
    }
}

class PlayingState : IPlayerState
{
    public string Name => "Playing";

    public void PlayPause(AudioPlayer player)
    {
        Console.WriteLine("⏸ Pause");
        player.SetState(new PausedState());
    }

    public void Stop(AudioPlayer player)
    {
        Console.WriteLine("⏹ Stop");
        player.SetState(new StoppedState());
    }
}

class PausedState : IPlayerState
{
    public string Name => "Paused";

    public void PlayPause(AudioPlayer player)
    {
        Console.WriteLine("▶️ Resume");
        player.SetState(new PlayingState());
    }

    public void Stop(AudioPlayer player)
    {
        Console.WriteLine("⏹ Stop from pause");
        player.SetState(new StoppedState());
    }
}
```

## Önemli Bilgi

State Pattern genellikle şu senaryolarda kullanılır:
- Bir object’in davranışı mevcut durumuna göre farklılaşmalıysa,  
- Durum geçişleri karmaşık hale gelmişse (çok fazla if–else veya switch varsa),  
- Davranış değişimlerini dinamik olarak yönetmek istiyorsan.  

Bu pattern sayesinde:  
✅ Kod okunabilirliği artar  
✅ Yeni durum eklemek kolaylaşır  
✅ State geçişleri merkezden değil, **her state’in kendi içinden** yönetilir  
