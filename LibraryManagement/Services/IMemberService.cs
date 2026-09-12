using LibraryManagement.Models;
using LibraryManagement.Utility;
using LibraryManagement.ViewModels;

namespace LibraryManagement.Services
{
    public interface IMemberService
    {
        Member? GetMemberInfo(string name);
        public List<Book> GetBooksList(int pageNum, int pageSize);
        public Book GetBookById(int Id);
        public List<Borrowing> GetMemberBooksList(Member member);
        public void IssueBook(Member member, Book book, DateTime expectedReturnDate);
        public void ReserveBook(int bookId,Member member);
        public List<Reservation> GetReservation(Member member);
        public void UpdatePassword(Member member, string password);
        public void ReturnBook(Member member,int Id);
        public List<AccountViewModel> GetUserAccount(Member member);
        Task<PaginatedList<BookViewModel>> GetAllFilter(string sortOrder, string currentFilter,bool? availability, string searchString, string searchAuthor, int? pageNumber, int pageSize);
    }
}
