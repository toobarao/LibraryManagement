using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class LibrarianController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        public LibrarianController(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;

        }
        public IActionResult Index()
        {
            var books = _dbcontext.Books.ToList();
            return View(books);
            
        }
        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            book.Availability = "Yes";
            _dbcontext.Books.Add(book);
            _dbcontext.SaveChanges();
            return View();
        }
        [HttpPost]
        public IActionResult RemoveBook(int Id)
        {

            Book book = _dbcontext.Books.Find(Id);
            _dbcontext.Books.Remove(book);
            _dbcontext.SaveChanges();
            var books = _dbcontext.Books.ToList();
            return View("Index",books);
        }

    }
}
