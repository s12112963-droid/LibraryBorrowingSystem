using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryBorrowingSystem.Models
{
    public class EBook: Book
    {
        public EBook(string title, string author, string isbn)
        : base(title, author, isbn)
        {
        }
        public override int GetBorrowDays()
        {
            return 7; 
        }
    }
}
