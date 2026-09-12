using LibraryManagement.Models;
using LibraryManagement.Services;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class LibrarianController(ILibrarianService librarianService) : Controller
    {

        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Index()
        {
            try
            {
            var books = librarianService.GetBooks();
            return View(books);
            }
            catch (Exception ex)
            {
                ErrorModel model = new();
                model.MakeErrorModel(ex);
                return RedirectToAction("Error", "Account", model);
            }

        }
        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    librarianService.AddBook(book);
                }
                return RedirectToAction("Index");
                
            }
            catch (Exception ex)
            {
                ErrorModel model = new();
                model.MakeErrorModel(ex);
                return RedirectToAction("Error", "Account", model);
            }
        }
        [HttpPost]
        public IActionResult RemoveBook(int Id)
        {
            try
            {

                librarianService.RemoveBook(Id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ErrorModel model = new();
                model.MakeErrorModel(ex);
                return RedirectToAction("Error", "Account", model);
            }
        }

        public IActionResult BorrowRequest()
        {
            try
            {
                var borrowList=librarianService.GetBorrowingsRequest();
            return View(borrowList);
            }
            catch (Exception ex)
            {
                ErrorModel model = new();
                model.MakeErrorModel(ex);
                return RedirectToAction("Error", "Account", model);
            }
        }
      
        public IActionResult UpdateBorrowRequest(int borrowId,int requestStatus)
        {
            try
            {
                librarianService.UpdateBorrowRequest(borrowId, requestStatus);

                return RedirectToAction("BorrowRequest");
            }
            catch (Exception ex)
            {
                ErrorModel model = new();
                model.MakeErrorModel(ex);
                return RedirectToAction("Error", "Account", model);
            }
        }

        public IActionResult GetBorrowBooks()
        {
            return View();
        }

       
        public IActionResult GetReservations()
        {
            return View();
        }

        public IActionResult GetReturnedBooks()
        {
            return View();
        }


    }
}
