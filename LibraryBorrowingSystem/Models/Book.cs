using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryBorrowingSystem.Models
{   //Id, Title, Author, ISBN, Availability status
    public abstract class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public bool IsAvailable { get; set; }

        public Book(string title, string author, string isbn)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            IsAvailable = true;
        }
        public abstract int GetBorrowDays();

      

    }
}
