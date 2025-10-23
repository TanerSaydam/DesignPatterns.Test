# Iterator Pattern

Iterator Pattern, bir koleksiyonun (örneğin liste, dizi veya set) içindeki object’lere  
koleksiyonun **iç yapısını bilmeden** sırasıyla erişmeyi sağlayan behavioral pattern’dir.  

Bu pattern sayesinde, verilerin nasıl saklandığı önemli olmadan (`array`, `list`, `tree` fark etmez),  
tüm elemanlar üzerinde aynı şekilde dolaşılabilir.

## Gerçek Hayat Analojisi

Bir kitap rafı düşün 📚  
Rafın nasıl yapıldığını (tahta mı, metal mi) bilmeden,  
sadece kitapları sırayla almak istersin.  

Raf = koleksiyon,  
kitapları sırayla veren kişi = iterator’dür.  

Iterator Pattern, bu “sırayla erişim” işini soyutlar.

## Program.cs (örnek kullanım)

```csharp
using System.Collections;

Console.WriteLine("Iterator Pattern...");

var library = new BookCollection();
library.AddBook(new Book("Clean Code"));
library.AddBook(new Book("Design Patterns"));
library.AddBook(new Book("Refactoring"));

foreach (var book in library)
{
    Console.WriteLine($"📘 {book.Title}");
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
```

## Önemli Bilgi

```csharp
public IEnumerator<Book> GetEnumerator() => new BookIterator(_books);
IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
```

Bu iki satır, .NET’te **Iterator Pattern**’ın kalbidir:

| Kod Satırı | Açıklama |
|-------------|-----------|
| `public IEnumerator<Book> GetEnumerator()` | Koleksiyonun `foreach (Book b in collection)` ile gezilebilmesini sağlar. Generic iterator’dür. |
| `IEnumerator IEnumerable.GetEnumerator()` | Non-generic `IEnumerable` desteği sağlar. Eski `foreach (var item in collection)` kodlarının da çalışmasını garantiler. |

Bu sayede `BookCollection` sınıfı, hem modern `IEnumerable<Book>` hem de klasik `IEnumerable` arayüzlerini destekler.  
Yani `foreach` yapısı, koleksiyonun **nasıl tutulduğunu bilmeden** onu gezebilir. ✅