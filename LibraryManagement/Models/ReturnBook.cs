namespace LibraryManagement.Models
{
    public class ReturnBook
    {
        public int Id { get; set; }
        public Borrowing Borrowing { get; set; }

        public DateTime ReturnDate { get; set; }
        public int MemberId { get; set; }

        public decimal Fine {  get; set; }

    }

    
}
