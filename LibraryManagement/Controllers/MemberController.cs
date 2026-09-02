using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    public class MemberController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        public MemberController(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IActionResult Index()
        {

            var books = _dbcontext.Books.ToList();
            return View(books);
        }
        public IActionResult IssueBook(int Id)
        {
            Book book = _dbcontext.Books.Find(Id);
            var username = HttpContext.Session.GetString("Username");
            Member member = _dbcontext.Memebers.Where(x => x.Name == username).First();
            checkMemeberBorrowStatus(Id);
            return View(book);

        }
        [HttpPost]
        public IActionResult IssueBook(int Id, DateTime returnDate)
        {
            Book book = _dbcontext.Books.Find(Id);
            var username = HttpContext.Session.GetString("Username");
            Member member = _dbcontext.Memebers.Where(x => x.Name == username).First();
            checkMemeberBorrowStatus(Id);
            _dbcontext.Borrowing.Add(new Borrowing(book, member, returnDate));
            _dbcontext.SaveChanges();
            book.updateBookAvailability();
            _dbcontext.Update(book);
            _dbcontext.SaveChanges();
            ViewBag.Msg = "Your Book is Issued";
            return RedirectToAction("Index");

        }

        public void checkMemeberBorrowStatus(int Id)
        {
            Book book = _dbcontext.Books.Find(Id);
            var username = HttpContext.Session.GetString("Username");
            Member member = _dbcontext.Memebers.Where(x => x.Name == username).First();
            if (book != null && member != null)
            {
                Borrowing borrow = new Borrowing();

                var memberBorrowList = _dbcontext.Borrowing.Where(x => x.Member.Id == member.Id).ToList();
                var memberReturnList = _dbcontext.ReturnBooks.Where(x => x.MemberId == member.Id).ToList();
                bool bookAvailabityStatus = book.getBookAvailability();
                ViewBag.CanIssue = true;

                if (!bookAvailabityStatus)
                {
                    ViewBag.Msg = "Sorry Book is not available at the moment...";
                    ViewBag.CanIssue = false;
                }
                bool maxLimitStatus = borrow.BorrowingLimitStatus(memberBorrowList);
                if (!maxLimitStatus)
                {
                    ViewBag.Msg = "You have reached to your borrow limits";
                    ViewBag.CanIssue = false;
                }
                decimal fineStatus = borrow.GetFineStatus(memberBorrowList, memberReturnList);
                if (fineStatus == -1)
                {
                    ViewBag.Msg = "You havent return your borrowed book";
                    ViewBag.CanIssue = false;
                }
                if (fineStatus > 0)
                {
                    ViewBag.Msg = $"You have fine of  {fineStatus} rupees.";
                    ViewBag.CanIssue = false;
                }


            }
        }

    }
}
