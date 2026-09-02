namespace LibraryManagement.Models
{
    public class IssueBook
    {
        public string MemberId { get; set; }
        public Book book { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
