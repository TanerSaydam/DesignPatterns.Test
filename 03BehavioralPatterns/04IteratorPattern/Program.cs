using System.Collections;

Console.WriteLine("Iterator Pattern...");

var library = new BookCollection();
library.AddBook(new Book("Clean Code"));
library.AddBook(new Book("Design Patterns"));
library.AddBook(new Book("Refactoring"));

foreach (var book in library)
{
    Console.WriteLine($"-- {book.Title}");
}

Console.ReadLine();

// ----- Domain -----
class Book
{
    public string Title { get; }
    public Book(string title) => Title = title;
}

// ----- Aggregate (Collection) -----
class BookCollection : IEnumerable<Book>
{
    private readonly List<Book> _books = new();

    public void AddBook(Book book) => _books.Add(book);

    public IEnumerator<Book> GetEnumerator() => new BookIterator(_books);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

// ----- Iterator -----
class BookIterator : IEnumerator<Book>
{
    private readonly List<Book> _books;
    private int _position = -1;

    public BookIterator(List<Book> books) => _books = books;

    public Book Current => _books[_position];

    object IEnumerator.Current => Current;

    public bool MoveNext()
    {
        _position++;
        return _position < _books.Count;
    }

    public void Reset() => _position = -1;

    public void Dispose() { }
}
