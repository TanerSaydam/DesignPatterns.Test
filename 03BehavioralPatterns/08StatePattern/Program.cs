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
