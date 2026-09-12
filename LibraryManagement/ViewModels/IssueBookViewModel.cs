namespace LibraryManagement.ViewModels
{
    public class IssueBookViewModel
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required DateTime ExpectedReturnDate { get; set; }
    }
}
