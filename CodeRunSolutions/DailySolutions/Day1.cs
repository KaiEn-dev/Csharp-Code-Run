using System;
using System.Collections.Generic;
using System.Text;

public class Day1 : ChallengeDay
{
    public Day1Library? ChallengeLibrary { get; set; }

    public Day1()
        : base(
            1,
            @"
Library System
---------------------
- Library
- Book
",
            @"Key Controls:
1. Recreate Library
2. Add Book
3. Show Library Info
4. Show Book List
5. Search Book
    1.Check Out
    2. Return
            ",
            30
        ) { }

    public override void InputInterface()
    {
        if (ChallengeLibrary == null)
        {
            LibraryCreationUI();
        }

        Console.WriteLine("\nEntry:");
        var action = Console.ReadLine();
        Console.WriteLine();

        switch (action)
        {
            case "1": // Create Library
                LibraryCreationUI();
                break;
            case "2": // Add Book
                AddBookUI();
                break;
            case "3": // Get Library Info
                ChallengeLibrary?.ShowLibraryInfo();
                break;
            case "4": // Get Library Book List
                ChallengeLibrary?.ShowBooks();
                break;
            case "5": // Search Book
                BookSearchUI();
                break;
            default:
                return;
        }
        InputInterface();
    }

    public void LibraryCreationUI()
    {
        Console.WriteLine("Create Library:");
        Console.Write("Name:");
        string? libraryName = Console.ReadLine();
        Console.Write("Address:");
        string? libraryAddress = Console.ReadLine();
        Console.Write("Operating Hours:");
        string? libraryHours = Console.ReadLine();

        ChallengeLibrary = new Day1Library(
            libraryName ?? "Unknown",
            libraryAddress ?? "-",
            libraryHours ?? "-"
        );

        Console.WriteLine("\n! Library Created");
    }

    public void AddBookUI()
    {
        Console.WriteLine("Adding Book");
        Console.WriteLine("Title:");
        string? bookTitle = Console.ReadLine();
        Console.WriteLine("Author:");
        string? bookAuthor = Console.ReadLine();

        Day1Book newBook = new Day1Book(bookTitle = "Unknown", bookAuthor = "-");
        Console.WriteLine();
        ChallengeLibrary?.AddBooks(newBook);

        newBook.PrintBookInfo();
    }

    public void BookSearchUI()
    {
        Console.WriteLine("Searching Book:");
        string? searchTitle = Console.ReadLine();

        var searchBook = ChallengeLibrary?.SearchByTitle(searchTitle ?? "nobook");

        if (searchBook != null)
        {
            Console.WriteLine("Action:");
            var entry = Console.ReadLine();

            switch (entry)
            {
                case "1":
                    searchBook.CheckOut();
                    break;
                case "2":
                    searchBook.Return();
                    break;
                default:
                    return;
            }
        }
    }
}

public class Day1Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public bool IsCheckedOut { get; set; } = false;

    public Day1Book(string title, string author)
    {
        this.Title = title;
        this.Author = author;
    }

    public void CheckOut()
    {
        this.IsCheckedOut = true;
        Console.WriteLine($"! Book({Title}) Checked Out");
    }

    public void Return()
    {
        this.IsCheckedOut = false;
        Console.WriteLine($"! Book({Title} Returned )");
    }

    public void PrintBookInfo()
    {
        Console.WriteLine(
            $@"
Title: {Title}
Author: {Author}
Status: {(IsCheckedOut ? "Unavailable" : "Available")}    
"
        );
    }
}

public class Day1Library
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string OperatingHours { get; set; }
    public Dictionary<string, Day1Book> Books { get; set; }

    public Day1Library(
        string name,
        string address,
        string operatingHours,
        Dictionary<string, Day1Book>? books = null
    )
    {
        this.Name = name;
        this.Address = address;
        this.OperatingHours = operatingHours;
        this.Books = books ?? [];
    }

    public void ShowLibraryInfo()
    {
        Console.WriteLine(
            @$"
{Name}
-----------------
Address: {Address}
Operating Hour: {OperatingHours}

        "
        );
    }

    public Day1Book? SearchByTitle(string title)
    {
        if (Books.ContainsKey(title))
        {
            Books[title].PrintBookInfo();
            return Books[title];
        }
        else
        {
            Console.WriteLine("! No Books Found.");
            return null;
        }
    }

    public void ShowBooks()
    {
        Console.WriteLine(
            @"
Book List
----------------

        "
        );
        int count = 1;
        foreach (var book in Books)
        {
            Console.WriteLine($"{count++}.");
            book.Value.PrintBookInfo();
        }
    }

    public void AddBooks(Day1Book book)
    {
        if (Books.ContainsKey(book.Title))
        {
            Console.WriteLine("! Book cannot be added");
        }
        else
        {
            Console.WriteLine("! Book added successfully");
            Books.Add(book.Title, book);
        }
    }
}
