using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryBorrowingSystem.Models
{
    public class PhysicalBook: Book
    {
        public PhysicalBook(string title, string author, string isbn)
        : base(title, author, isbn)
        {
        }

       
            public override int GetBorrowDays()
            {
                return 14;
            }
    }
}
