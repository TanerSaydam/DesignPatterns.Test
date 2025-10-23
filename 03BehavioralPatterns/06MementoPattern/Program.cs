Console.WriteLine("Memento Pattern - Text Editor Undo/Redo...");

var editor = new TextEditor();
var history = new History();

editor.Type("Hello");
history.Save(editor);

editor.Type(", World");
history.Save(editor);

editor.Type("! This is a test.");
Console.WriteLine($"Current: {editor.Content}");

history.Undo(editor);
Console.WriteLine($"After Undo: {editor.Content}");

history.Redo(editor);
Console.WriteLine($"After Redo: {editor.Content}");

Console.ReadLine();

// Originator
class TextEditor
{
    public string Content { get; private set; } = "";

    public void Type(string text) => Content += text;

    public EditorMemento CreateSnapshot() => new EditorMemento(Content);

    public void Restore(EditorMemento memento) => Content = memento.State;
}

// Memento (immutable)
class EditorMemento
{
    public string State { get; }
    public DateTime CreatedAt { get; } = DateTime.Now;

    public EditorMemento(string state) => State = state;
}

// Caretaker
class History
{
    private readonly Stack<EditorMemento> _undo = new();
    private readonly Stack<EditorMemento> _redo = new();

    public void Save(TextEditor editor)
    {
        _undo.Push(editor.CreateSnapshot());
        _redo.Clear();
    }

    public void Undo(TextEditor editor)
    {
        if (_undo.Count == 0) return;
        var current = editor.CreateSnapshot();
        var prev = _undo.Pop();
        _redo.Push(current);
        editor.Restore(prev);
    }

    public void Redo(TextEditor editor)
    {
        if (_redo.Count == 0) return;
        var current = editor.CreateSnapshot();
        var next = _redo.Pop();
        _undo.Push(current);
        editor.Restore(next);
    }
}
