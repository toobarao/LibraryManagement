using AutoMapper;
using LibraryManagement.Models;
using LibraryManagement.Utility;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Services
{
    public class MemberService(ApplicationDbContext dbcontext, IMapper mapper) : IMemberService
    {
        public Member? GetMemberInfo(string name)
        {

            Member member = dbcontext.Memebers.Where(x => x.Name == name).FirstOrDefault();
            return member;

        }
        public List<Book> GetBooksList(int pageNum, int pageSize)
        {

            var books = dbcontext.Books.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
            return books;

        }
        public Book GetBookById(int Id)
        {
            Book book = dbcontext.Books.Find(Id);
            return book;
        }

        public List<Borrowing> GetMemberBooksList(Member member)
        {
            var books = dbcontext.Borrowing.Where(x => x.Member.Id == member.Id && x.RequestStatus==1&&(x.BorrowStatus==1|| x.BorrowStatus == 4)).Include(x => x.Book).ToList();
            return books;
        }

        public void IssueBook(Member member, Book book, DateTime expectedReturnDate)
        {
            var borrowBook = new Borrowing(book,member,expectedReturnDate);
            dbcontext.Borrowing.Add(borrowBook);
            book.updateBookAvailability();
            dbcontext.Books.Update(book);
            dbcontext.SaveChanges();
          
        }

        public async Task<PaginatedList<BookViewModel>> GetAllFilter(string sortOrder, string currentFilter,bool? availability, string searchString,string searchAuthor, int? pageNumber, int pageSize)
        {
            if (searchString != null|| searchAuthor!=null)
            {
                pageNumber = 1;
            }
            else
            {

                searchString = currentFilter;
            }

            var books = from m in dbcontext.Books select m;
            if(availability.HasValue)
            {string val=availability.Value==true?"Yes":"No";
                books = books.Where(s => s.Availability == val);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title!.Contains(searchString)
                || s.Author!.Contains(searchString)
                || s.Publisher!.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(searchAuthor))
            {
                books = books.Where(s => s.Author!.Contains(searchAuthor));
            }
                books = sortOrder switch
            {
                "id" => books.OrderBy(s => s.Id),
                "title_desc" => books.OrderByDescending(s => s.Title),
                "title" => books.OrderBy(s => s.Title),
                "author_desc" => books.OrderByDescending(s => s.Author),
                "author" => books.OrderBy(s => s.Author),
                
                _ => books.OrderBy(s => s.Title),
            };

            return PaginatedList<BookViewModel>.Create(mapper.Map<IEnumerable<BookViewModel>>(await books.ToListAsync()), pageNumber ?? 1, pageSize);
        }

        public void ReserveBook(int bookId, Member member)
        {
            Book book=dbcontext.Books.Find(bookId);
            if (book != null&& member!=null) { 
            dbcontext.Reservations.Add(new Reservation(book, member));
            dbcontext.SaveChanges();
            
            }
        }

        public List<Reservation> GetReservation(Member member)
        {
           return dbcontext.Reservations.Where(x=>x.member.Id== member.Id).Include(x=>x.book).ToList();
        }

        public void UpdatePassword(Member member, string password)
        {
           var hashPassword=new PasswordHasher<Member>();
           
            member.Password = hashPassword.HashPassword(member, password);

            dbcontext.Update(member);

            dbcontext.SaveChanges();

        }

        public void ReturnBook(Member member, int Id)
        {
            Borrowing borrow = dbcontext.Borrowing.Where(x => x.Id == Id).Include(x => x.Book).FirstOrDefault();
            if (borrow != null&& member!=null) {
                //fine calculation
                var book=dbcontext.Books.Find(borrow.Book.Id);
                book.updateBookAvailability();
                decimal fine=borrow.CalculateFine(member);
                borrow.updateBorrowStatus();
                dbcontext.ReturnBooks.Add(new ReturnBook(borrow,member.Id,fine));
                dbcontext.Books.Update(book);
                dbcontext.Borrowing.Update(borrow);
                dbcontext.SaveChanges();
            }
           
        }

        public List<AccountViewModel> GetUserAccount(Member member)
        {
            var returnBook=dbcontext.ReturnBooks.Where(x => x.MemberId == member.Id).ToList();
            var borrowBooks=dbcontext.Borrowing.Where(x=>x.Member.Id == member.Id&&x.RequestStatus==1).Include(x=>x.Book).ToList();
            List<AccountViewModel> model=new List<AccountViewModel>();
            //1 & 2
            foreach (var borrow in borrowBooks)
            {
               if(borrow.BorrowStatus==1||borrow.BorrowStatus==2)
                {
                    if (borrow.BorrowStatus == 2)
                    {
                        var returned = returnBook.Where(x => x.Borrowing.Id == borrow.Id).FirstOrDefault();
                        if (returned != null)
                        {
                            AccountViewModel account = new AccountViewModel();
                            account.BorrowId = borrow.Id;
                            account.BorrowDate = borrow.BorrowDate;
                            account.ReturnedDate = returned.ReturnDate;
                            account.Fine = returned.Fine;
                            account.BookAuthor = borrow.Book.Author;
                            account.BookName = borrow.Book.Title;
                            model.Add(account);
                        }
                    }
                    else
                    {
                        AccountViewModel account = new AccountViewModel();
                        account.BorrowId = borrow.Id;
                        account.BorrowDate = borrow.BorrowDate;
                        account.BookAuthor = borrow.Book.Author;
                        account.BookName = borrow.Book.Title;
                        model.Add(account);
                    }
                }
            }

            return model;

        }
    }
}
