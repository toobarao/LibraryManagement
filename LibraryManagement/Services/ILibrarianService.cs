using LibraryManagement.Models;
using LibraryManagement.ViewModels;

namespace LibraryManagement.Services
{
    public interface ILibrarianService
    {
        public List<Book> GetBooks();

        public void AddBook(Book book);
        public void RemoveBook(int Id);
        public void UpdateBorrowRequest(int borrowId, int requestStatus);

        public List<Borrowing> GetBorrowingsRequest();
    }
}
