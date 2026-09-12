using LibraryManagement.Models;
using LibraryManagement.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Services
{
    public class LibrarianService(ApplicationDbContext dbcontext) : ILibrarianService
    {
        public void AddBook(Book book)
        {
            dbcontext.Books.Add(book);
            dbcontext.SaveChanges();
        }

        public List<Book> GetBooks()
        {
            return dbcontext.Books.ToList();
        }

        public List<Borrowing> GetBorrowingsRequest()
        {
           return dbcontext.Borrowing.Where(x=>x.RequestStatus==3).Include(x=>x.Book).Include(x=>x.Member).ToList();
        }

        public void RemoveBook(int Id)
        {
            var book = dbcontext.Books.FirstOrDefault(x => x.Id == Id);
            if (book != null) {

                dbcontext.Remove(book);
                dbcontext.SaveChanges();
            }
            else
            {
                throw new Exception("Book not Found");
            }
        }

        public void UpdateBorrowRequest(int borrowId, int requestStatus)
        {
            Borrowing borrowing = dbcontext.Borrowing.FirstOrDefault(x => x.Id == borrowId);
            borrowing.RequestStatus = requestStatus;
            borrowing.BorrowDate = DateTime.Now;
            borrowing.BorrowStatus = 4;
            dbcontext.SaveChanges();
        }
    }
}
