using System;
using System.Collections.Generic;


namespace Exercise1
{
    class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }

    } //End Class Book
    class Program
    {
        static void Main(string[] args)
        {
            Stack<Book> books = new Stack<Book>();
            string userInput = " ";

            while (userInput != "5")
            {
                Console.WriteLine(@"
Please Select an option below:

[1] Add Textbook
[2] Read Textbook
[3] See Textbook Details
[4] Return Textbooks to Shelf
[5] Exit

Enter choice:");

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        AddTextbook(books);
                        break;
                    case "2":
                        ReadTextbook(books);
                        break;
                    case "3":
                        SeeTextbookDetails(books);
                        break;
                    case "4":
                        ReturnTextbooksToShelf(books);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Unrecognized Command");
                        break;
                } //End Switch
            }
        } //End Main()

        static void AddTextbook(Stack<Book> books)
        {
            Book newBook = new Book();

            do
            {
                Console.WriteLine("Enter Title of the Textbook: ");
                newBook.Title = Console.ReadLine();
            } while (newBook.Title.Trim().Length == 0);

            do
            {
                Console.WriteLine("Enter Author of the Book: ");
                newBook.Author = Console.ReadLine();
            } while (newBook.Author.Trim().Length == 0);

            books.Push(newBook);
        } // End AddTextbook()

        static void ReadTextbook(Stack<Book> books)
        {
            Book top;

            if (books.TryPop(out top))
            {
                Console.WriteLine($"Reading the book with the title {top.Title} by {top.Author}");
            }
            else
            {
                Console.WriteLine("No books to read!");
            }
        } //End ReadTextbook()

        static void SeeTextbookDetails(Stack<Book> books)
        {
            Book top;

            if (books.TryPeek(out top))
            {
                Console.WriteLine($"This book is by {top.Author}");
            }
            else
            {
                Console.WriteLine("No books to look at!");
            }
        } //End SeeTextbookDetails()

        static void ReturnTextbooksToShelf(Stack<Book> books)
        {
            Book top;

            if (books.Count > 0)
            {
                while (books.TryPop(out top))
                {
                    Console.WriteLine($"Title: {top.Title}");
                }
            }
            else
            {
                Console.WriteLine("No books to return to shelf!");
            }
        } //End ReturnTextbookstoShelf()
    }
}