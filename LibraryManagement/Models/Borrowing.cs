using LibraryManagement.Migrations;

namespace LibraryManagement.Models
{
    public class Borrowing
    {
        public int Id { get; set; }
        public DateTime ExpectedReturnDate { get; set; }

        public Book Book { get; set; }
        public Member Member { get; set; }

        public DateTime BorrowDate { get; set; }

        public BorrowingStatus Status { get; set; }
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
        public bool BorrowingLimitStatus(List<Borrowing> borrowList,Member member)
        {
            var booksBorrowed = borrowList.Count();
            
            if (booksBorrowed < member.borrowLimit())
                return true;
            return false;

        }
        public decimal GetFineStatus(List<Borrowing> borrowList,List<ReturnBook> returnList,Member member)
        {
            int fineValue = member.FineValue();
            decimal fine = 0;
            if (borrowList.Count() == 0 && returnList.Count() == 0)
                return fine;
            else
            {
                foreach (var borrowItem in borrowList)
                {
                    var borrowId=borrowItem.Id;
                    var expectedReturnDate=borrowItem.ExpectedReturnDate;
                    int lateDays= (DateTime.Now - expectedReturnDate).Days;
                    if (lateDays>0 && returnList.Count()>0)
                    {
                        var bookReturn = returnList.Where(x => x.Borrowing.Id == borrowId).First();
                        if (bookReturn != null)
                        {
                            int days = (bookReturn.ReturnDate - expectedReturnDate).Days;
                            if (days > 0)
                            {
                                fine += days * fineValue;
                            }
                        }
                        else
                            return -1;
                    }
                    else if(lateDays <= 0 && returnList.Count() == 0)
                    {
                        return 0;
                    }
                    else
                        return -1;




                }
              


                return fine;
            }
           

        }

        public decimal CalculateFine(DateTime returnDate, Member member)
        {
            int fineValue = member.FineValue();
            int days=(returnDate-this.ExpectedReturnDate).Days;
            if(days>0)
            {
                return days*fineValue;
            }
            return 0;
        }

        public void updateBorrowStatus(DateTime returnDate)
        {
            int days = (returnDate - this.ExpectedReturnDate).Days;
            if (days>0)
                this.Status = BorrowingStatus.ReturnedLate;
            else
                this.Status= BorrowingStatus.ReturnedOnTime;


        }
    }

    public enum BorrowingStatus
    {
        Borrowed,
        ReturnedOnTime,
        ReturnedLate,
        Overdue
    }
}
