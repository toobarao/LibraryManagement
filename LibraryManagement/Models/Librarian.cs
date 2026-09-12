using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Librarian
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public Librarian(string name,string email) {
            this.Name = name;
            this.Email = email;

        }
        
        public void AddBook(Book book)
        {

        }
        public void RemoveBook(Book book)
        {

        }
        public void RegisterMember(Member member)
        {

        }

        public Book Issue(Member member, Book book)
        {
            return null;
        }
        public void Return(Member member, Book book)
        {

        }
        public void ViewRecords()
        {

        }

    }
}
