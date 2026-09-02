namespace LibraryManagement.Models
{
    public class Borrowing
    {
        public int Id { get; set; }
        public DateTime ExpectedReturnDate { get; set; }

        public Book Book { get; set; }
        public Member Member { get; set; }

        public DateTime BorrowDate { get; set; }

        public int MaxBorrowLimit = 3;

        public Borrowing() { }

        public  Borrowing(Book book,Member member,DateTime returnDate)
        {
            this.Book = book;
            this.Member = member;
            this.BorrowDate=DateTime.Now;
            this.ExpectedReturnDate=returnDate;
        }

        public void UpdateBookStatus(Book book)
        {
            if (book.Availability == "Yes")
                book.Availability = "No";
            else
                book.Availability = "Yes";

        }
        public bool BorrowingLimitStatus(List<Borrowing> borrowList)
        {
            var booksBorrowed = borrowList.Count();
            
            if (booksBorrowed < MaxBorrowLimit)
                return true;
            return false;

        }
        public decimal GetFineStatus(List<Borrowing> borrowList,List<ReturnBook> returnList)
        {
            decimal fine = 0;
            if (borrowList.Count() == 0 && returnList.Count() == 0)
                return fine;
            else
            {
                foreach (var borrowItem in borrowList)
                {
                    var borrowId=borrowItem.Id;
                    var expectedReturnDate=borrowItem.ExpectedReturnDate;
                    var bookReturn = returnList.Where(x => x.Borrowing.Id == borrowId).First();
                    if (bookReturn!=null)
                    {
                        int days = (bookReturn.ReturnDate - expectedReturnDate).Days;
                        if (days>0)
                        {
                            fine += days * 20;
                        }
                    }
                    else
                        return -1;

                    
                }
              


                return fine;
            }
           

        }

        public decimal CalculateFine(DateTime returnDate)
        {
            return 67;
        }
    }
}
