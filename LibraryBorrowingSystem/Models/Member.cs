using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryBorrowingSystem.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
       
        public Member(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}
