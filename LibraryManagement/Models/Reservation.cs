namespace LibraryManagement.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public Book book {  get; set; } 
        public Member member { get; set; }

        public DateTime ResevationDate { get; set; } = DateTime.Now;

        public Reservation() { }

        public Reservation(Book book,Member member)
        {
            this.book = book;
            this.member = member;
        }

    }

}
