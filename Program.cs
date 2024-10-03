using System;
using System.Collections.Generic;


public class Book
{
    public string Title { get; set; }

    public Book(string title)
    {
        Title = title;
    }
}


public class Author
{
    public string Name { get; set; }

    public Author(string name)
    {
        Name = name;
    }
}


public class Library
{
    private Dictionary<Author, List<Book>> authors = new Dictionary<Author, List<Book>>();


    public void AddBook(string authorName, string bookTitle)
    {

        bool flag = false;
        foreach (var elem in authors)
        {
            if (elem.Key.Name == authorName)
            {
                elem.Value.Add(new Book(bookTitle));
                flag = true;
                break;
            }
        }
        if (!flag)
        {
            Author author = new Author(authorName);
            authors[author] = new List<Book>();
            authors[author].Add(new Book(bookTitle));
        }
        Console.WriteLine($"Книга \"{bookTitle}\" добавлена для автора {authorName}.");
    }

    public void RemoveBook(string authorName, string bookTitle)
    {

        Author author = authors.Keys.FirstOrDefault(a => a.Name == authorName);

        if (author != null)
        {
            var bookToRemove = authors[author].Find(b => b.Title == bookTitle);
            if (bookToRemove != null)
            {
                authors[author].Remove(bookToRemove);
                Console.WriteLine($"Книга \"{bookTitle}\" удалена у автора {authorName}.");
            }
            else
            {
                Console.WriteLine($"Книга \"{bookTitle}\" не найдена у автора {authorName}.");
            }
        }
        else
        {
            Console.WriteLine($"Автор {authorName} не найден.");
        }
    }


    public void Display(string authorName)
    {
        Author author = new Author(authorName);
        bool flag = false;
        foreach (var elem in authors)
        {
            if (elem.Key.Name == authorName)
            {
                flag = true;
                Console.WriteLine($"Книги автора {authorName}:");
                foreach (var book in elem.Value)
                {
                    Console.WriteLine($"- {book.Title}");
                }
                   
            }
        }
        if (!flag)
            Console.WriteLine($"Автор {authorName} не найден.");
    }

    public void FindBook(string title)
    {
        bool found = false;
        foreach (var entry in authors)
        {
            var book = entry.Value.Find(b => b.Title == title);
            if (book != null)
            {
                Console.WriteLine($"Книга \"{title}\" найдена у автора {entry.Key.Name}.");
                found = true;
                break;
            }
        }
        if (!found)
            Console.WriteLine($"Книга \"{title}\" не найдена.");
    }


    public void DisplayAllBooks()
    {
        Console.WriteLine("Полный список книг в библиотеке:");
        foreach (var entry in authors)
        {
            Console.WriteLine($"Автор: {entry.Key.Name}");
            foreach (var book in entry.Value)
                Console.WriteLine($"- {book.Title}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();

        library.AddBook("J.K. Rowling", "Harry Potter and the Sorcerer's Stone");
        library.AddBook("J.K. Rowling", "Harry Potter and the Chamber of Secrets");
        library.AddBook("George Orwell", "1984");
        library.AddBook("George Orwell", "Animal Farm");

        Console.WriteLine();

        library.Display("J.K. Rowling");
        Console.WriteLine();

        library.FindBook("Animal Farm");
        Console.WriteLine();

        library.RemoveBook("J.K. Rowling", "Harry Potter and the Chamber of Secrets");
        Console.WriteLine();

        library.DisplayAllBooks();
    }
}
