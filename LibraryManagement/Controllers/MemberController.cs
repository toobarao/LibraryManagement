using AutoMapper;
using LibraryManagement.Migrations;
using LibraryManagement.Models;
using LibraryManagement.Services;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace LibraryManagement.Controllers
{
    [Authorize(Roles = "PremiumMember,StudentMember")]
    public class MemberController(IMemberService memberService, IMapper mapper) : Controller
    {


        public async  Task<IActionResult> Index(string sortOrder,bool? availability, string currentFilter, string searchString, string searchAuthor, int? pageNumber)
        {
            try
            {
                ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder.Equals("title") ? "title_desc" : "";
                ViewData["AuthorSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder.Equals("author") ? "author_desc" : "";
                ViewData["Availability"] = availability.HasValue ? availability.Value : false;
                ViewData["CurrentFilter"] = searchString;
                ViewData["Author"] = searchAuthor; 
                ViewData["CurrentSort"] = sortOrder;

                return View(await memberService.GetAllFilter(sortOrder, currentFilter, availability, searchString, searchAuthor, pageNumber, 10));
               // var books = memberService.GetBooksList(pageNum, pageSize);

               
            }
            catch (Exception ex)
            {
                ErrorModel model = new();
                model.MakeErrorModel(ex);
                return RedirectToAction("Error", "Account", model);
            }
           
        }
        public IActionResult IssueBook(int Id)
        {
            try
            {
                Book book = memberService.GetBookById(Id);
                IssueBookViewModel issueBookViewModel = mapper.Map<Book, IssueBookViewModel>(book);
                string username = User.Identity.Name;
                Member member = memberService.GetMemberInfo(username);
                ViewBag.CanIssue = true;
                if (!book.getBookAvailability())
                {
                    ViewBag.AvailabilityMsg = "Sorry Book is not available at the moment...";
                    ViewBag.CanIssue = false;
                }
                //fine
                //reservation
                //borrow limit

                // checkMemeberBorrowStatus(Id);
                return View(issueBookViewModel);
            }
            catch (Exception ex)
            {
                ErrorModel model = new();
                model.MakeErrorModel(ex);
                return RedirectToAction("Error", "Account", model);
            }
        }
        [HttpPost]
        public IActionResult IssueBook(IssueBookViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Book book = memberService.GetBookById(model.Id);
                    string username = User.Identity.Name;
                    Member member = memberService.GetMemberInfo(username);
                    if (member != null && book != null)
                    {
                        memberService.IssueBook(member, book, model.ExpectedReturnDate);
                        ViewBag.Msg = "Your Book is Issued";
                        return RedirectToAction("Index");
                    }
                    throw new Exception("Member or Book not found");
                }
                else { 
                return View(model);
                }
            }
            catch (Exception ex)
            {
                ErrorModel error= new();
                error.MakeErrorModel(ex);
                return RedirectToAction("Error", "Account", error);
            }

        }
        public IActionResult BorrowBooks()
        {
            try
            {
                string username = User.Identity.Name;
                Member member = memberService.GetMemberInfo(username);
                var borrowBookList = memberService.GetMemberBooksList(member);
                if (borrowBookList.Count == 0)
                    return View(new List<Borrowing>());
                return View(borrowBookList); }
              catch (Exception ex)
                {
                    ErrorModel error = new();
                    error.MakeErrorModel(ex);
                    return RedirectToAction("Error", "Account", error);
                }
        }
       
        public IActionResult ReturnBook(int Id)
        {
            string username = User.Identity.Name;
            Member member = memberService.GetMemberInfo(username);
            memberService.ReturnBook(member,Id);
            ViewBag.ReturnMsg = "Your book is returned";
            return RedirectToAction("BorrowBooks");

        }


        public void checkMemeberBorrowStatus(int Id)
        {
            //Book book = _dbcontext.Books.Find(Id);
            //var username = HttpContext.Session.GetString("Username");
            //Member member = _dbcontext.Memebers.Where(x => x.Name == username).First();
            //if (book != null && member != null)
            //{
            //    Borrowing borrow = new Borrowing();

            //    var memberBorrowList = _dbcontext.Borrowing.Where(x => x.Member.Id == member.Id).ToList();
            //    var memberReturnList = _dbcontext.ReturnBooks.Where(x => x.MemberId == member.Id).ToList();
            //    bool bookAvailabityStatus = book.getBookAvailability();
            //    ViewBag.CanIssue = true;

            //    if (!bookAvailabityStatus)
            //    {
            //        ViewBag.AvailabilityMsg = "Sorry Book is not available at the moment...";
            //        ViewBag.CanIssue = false;
            //    }
            //    bool maxLimitStatus = borrow.BorrowingLimitStatus(memberBorrowList, member);
            //    if (!maxLimitStatus)
            //    {
            //        ViewBag.BorrowLimitMsg = "You have reached to your borrow limits";
            //        ViewBag.CanIssue = false;
            //    }
            //    decimal fineStatus = borrow.GetFineStatus(memberBorrowList, memberReturnList, member);
            //    if (fineStatus == -1)
            //    {
            //        ViewBag.UnreturnBooksMsg = "You havent return your borrowed book";
            //        ViewBag.CanIssue = false;
            //    }
            //    if (fineStatus > 0)
            //    {
            //        ViewBag.UnPaidFineMsg = $"You have fine of  {fineStatus} rupees.";
            //        ViewBag.CanIssue = false;
            //    }


            //}
        }
        public IActionResult Account()
        {
            var username = User.Identity.Name;
            Member member = memberService.GetMemberInfo(username);
           List<AccountViewModel> model= memberService.GetUserAccount(member);
            return View(model);  
        }


        public IActionResult Profile()
        {
            try
            {
                var username = User.Identity.Name;
                var usertype = User.Identity.AuthenticationType;
                Member member = memberService.GetMemberInfo(username);
                ProfileViewModel profileViewModel = mapper.Map<Member, ProfileViewModel>(member);

                return View(profileViewModel);
            }
            catch (Exception ex)
            {
                ErrorModel model = new();
                model.MakeErrorModel(ex);
                return RedirectToAction("Error", "Account", model);
            }


        }
        [HttpPost]
        public IActionResult Profile(ProfileViewModel profileViewModel)
        {
            try
            {
                if (ModelState.IsValid) { 
                
                
                
                }
                return View(profileViewModel);
            }
            catch (Exception ex)
            {
                ErrorModel model = new();
                model.MakeErrorModel(ex);
                return RedirectToAction("Error", "Account", model);
            }
        }
        public IActionResult GetMemberResrvations()
        {
            try {
                var username = User.Identity.Name;
                Member member = memberService.GetMemberInfo(username);
                var model=memberService.GetReservation(member);
                return View(model);
            }
            catch (Exception ex)
            {
                ErrorModel model = new();
                model.MakeErrorModel(ex);
                return RedirectToAction("Error", "Account", model);
            }

        }

      
        public IActionResult ReserveBook(int id)
        {
            try
            {

                var username = User.Identity.Name;
                Member member = memberService.GetMemberInfo(username);
                memberService.ReserveBook(id, member);
                return Json(new {success=true});
            }
            catch (Exception ex)
            {
                ErrorModel model = new();
                model.MakeErrorModel(ex);
                return Json(new { success= false });
             
            }
        }

        public IActionResult UpdatePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UpdatePassword(UpdatePasswordViewModel model)
        {
            try { 
            if (ModelState.IsValid&& model.Password.Equals(model.ConfirmPassword)) {
                var username = User.Identity.Name;
                Member member = memberService.GetMemberInfo(username);
                memberService.UpdatePassword(member, model.Password);
                ViewBag.Msg = "Your Password is Update";
            }
                return View();
            }
            catch (Exception ex)
            {
                ErrorModel errorModel = new();
                errorModel.MakeErrorModel(ex);
                return RedirectToAction("Error", "Account", errorModel);
            }
        }

    }
}
