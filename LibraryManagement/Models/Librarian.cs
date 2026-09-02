namespace LibraryManagement.Models
{
    public class Librarian
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Password { get; set; }

        public Librarian( string name,string password)
        {
           
            this.Name = name;
            this.Password = password;   
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
