using LibraryBorrowingSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryBorrowingSystem.Services
{
    public class LibraryService
    {
       private List<Book> books=new List<Book>();
       private List<Member> members=new List<Member>();


        public void AddBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book), "Book cannot be null");
            }
            books.Add(book);
        }

        public void RegisterMember(Member member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member), "Member cannot be null");
            }
            members.Add(member);
        }

        public List<Book> GetAvailableBooks()
        {
            //return books.Where(b => b.IsAvailable).ToList();
            return books.FindAll(b => b.IsAvailable);
        }
       

        public bool BorrowBook(int bookId, int memberId)
        {
            //var book = books.FirstOrDefault(b => b.Id == bookId);
            var book = books.Find(b => b.Id == bookId);

            var member = members.Find(m=> m.Id== memberId);
            if (book == null)
            {
                Console.WriteLine("Book not found");
                return false;
            }

            if (member == null)
            {
                Console.WriteLine("Member not found");
                return false;
            }

            if (!book.IsAvailable)
            {
                Console.WriteLine("Book is not available");
                return false;
            }

            book.IsAvailable = false; // Mark the book as borrowed
            var borrowDays = book.GetBorrowDays(); 
            return true;
            

        }

        public bool ReturnBook(int bookId)
        {
            var book = books.Find(b => b.Id == bookId);
            if (book == null)
            {
                return false;
            }
            if (book.IsAvailable)
            {
                return false; // Book is already available, cannot return
            }
            book.IsAvailable = true;
            return true;
        }

        public List<Book> SearchByTitle(string title)
        {
            return books.FindAll(b => b.Title.ToLower().Contains(title.ToLower()));
        }

        public List<Book> SearchByAuthor(string author)
            {
                return books.FindAll(b => b.Author.ToLower().Contains(author.ToLower()));
            }
    
            public List<Book> SearchByISBN(string isbn)
            {
                return books.FindAll(b => b.ISBN.ToLower().Contains(isbn.ToLower()));
            }

    }
}
