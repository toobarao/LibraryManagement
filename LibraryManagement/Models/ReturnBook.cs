namespace LibraryManagement.Models
{
    public class ReturnBook
    {
        public int Id { get; set; }
        public Borrowing Borrowing { get; set; }

        public DateTime ReturnDate { get; set; }
        public int MemberId { get; set; }

        public decimal Fine {  get; set; } = 0;
        public ReturnBook() { }

        public ReturnBook(Borrowing borrow, DateTime returnDate,int memberId,decimal fine)
        {
            this.Borrowing = borrow;
            this.ReturnDate = returnDate;
            this.MemberId = memberId;
            this.Fine = fine;
        }

    }
    
    
}
