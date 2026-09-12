namespace LibraryManagement.ViewModels
{
    public class AccountViewModel
    {
        public int BorrowId {  get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }

        public DateTime? BorrowDate { get; set; }
        public DateTime? ReturnedDate { get; set; }

       

        public decimal Fine {  get; set; }
    }
}
