// See https://aka.ms/new-console-template for more information


using LibraryBorrowingSystem.Models;
using LibraryBorrowingSystem.Services;
using System;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;


class Program
{
    static void Main(string[] args)
    {
        var libraryService = new LibraryService();

        bool running = true;
        while (running)
        {
            Console.WriteLine("====== Library System ======");
            Console.WriteLine("1 - Add Physical Book");
            Console.WriteLine("2 - Add EBook");
            Console.WriteLine("3 - Register Member");
            Console.WriteLine("4 - Borrow Book");
            Console.WriteLine("5 - Return Book");
            Console.WriteLine("6 - View Available Books");
            Console.WriteLine("7 - Exit");

            Console.Write("Choose option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    {
                        string title = ReadNonEmptyString("Enter title: ");
                        string author = ReadNonEmptyString("Enter author: ");
                        string isbn = ReadNonEmptyString("Enter ISBN: ");

                        Console.Write("Book type (1 = Physical, 2 = Ebook): ");
                        string type = Console.ReadLine();

                        Book book;

                        if (type == "1")
                            book = new PhysicalBook(title, author, isbn);
                        else if (type == "2")
                            book = new EBook(title, author, isbn);
                        else
                        {
                            Console.WriteLine("Invalid book type");
                            Pause();
                            break;
                        }

                        libraryService.AddBook(book);
                        Console.WriteLine("Book added successfully");

                        Pause();
                        break;
                    }

                case "2":
                    {
                        int memberId = ReadInt("Enter member id: ");
                        string name = ReadNonEmptyString("Enter member name: ");
                        string email = ReadNonEmptyString("Enter member email: ");

                        Member member = new Member(memberId, name, email);
                        libraryService.RegisterMember(member);

                        Console.WriteLine("Member registered successfully");

                        Pause();
                        break;
                    }

                case "3":
                    {
                        int bookId = ReadInt("Enter book id: ");
                        int memberId = ReadInt("Enter member id: ");

                        bool borrowed = libraryService.BorrowBook(bookId, memberId);

                        if (borrowed)
                            Console.WriteLine("Book borrowed successfully");
                        else
                            Console.WriteLine("Borrow failed");

                        Pause();
                        break;
                    }

                case "4":
                    {
                        int bookId = ReadInt("Enter book id: ");

                        bool returned = libraryService.ReturnBook(bookId);

                        if (returned)
                            Console.WriteLine("Book returned successfully");
                        else
                            Console.WriteLine("Return failed");

                        Pause();
                        break;
                    }

                case "5":
                    {
                        Console.WriteLine("Search by:");
                        Console.WriteLine("1. Title");
                        Console.WriteLine("2. Author");
                        Console.WriteLine("3. ISBN");

                        Console.Write("Choose option: ");
                        string searchChoice = Console.ReadLine();

                        List<Book> results = new List<Book>();

                        if (searchChoice == "1")
                        {
                            string title = ReadNonEmptyString("Enter title: ");
                            results = libraryService.SearchByTitle(title);
                        }
                        else if (searchChoice == "2")
                        {
                            string author = ReadNonEmptyString("Enter author: ");
                            results = libraryService.SearchByAuthor(author);
                        }
                        else if (searchChoice == "3")
                        {
                            string isbn = ReadNonEmptyString("Enter ISBN: ");
                            results = libraryService.SearchByISBN(isbn);
                        }
                        else
                        {
                            Console.WriteLine("Invalid search option");
                            Pause();
                            break;
                        }

                        if (results.Count == 0)
                            Console.WriteLine("No books found");
                        else
                        {
                            foreach (var book in results)
                                Console.WriteLine($"{book.Id} | {book.Title} | {book.Author} | {book.ISBN}");
                        }

                        Pause();
                        break;
                    }

                case "6":
                    {
                        var availableBooks = libraryService.GetAvailableBooks();

                        if (availableBooks.Count == 0)
                            Console.WriteLine("No available books");
                        else
                        {
                            foreach (var book in availableBooks)
                                Console.WriteLine($"{book.Id} | {book.Title} | {book.Author} | {book.ISBN}");
                        }

                        Pause();
                        break;
                    }

                case "7":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid option");
                    Pause();
                    break;
            }
        }
    }

    static int ReadInt(string message)
    {
        int number;
        Console.Write(message);

        while (!int.TryParse(Console.ReadLine(), out number))
        {
            Console.Write("Invalid number, try again: ");
        }

        return number;
    }

    static string ReadNonEmptyString(string message)
    {
        Console.Write(message);
        string input = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(input))
        {
            Console.Write("Value cannot be empty, try again: ");
            input = Console.ReadLine();
        }

        return input;
    }

    static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }
}