using LibraryManagement.Models;
using LibraryManagement.ViewModels;

namespace LibraryManagement.Services
{
    public interface IAccountService
    {
        bool Login(LoginViewModel loginViewModel);
        bool Register(RegisterViewModel registerViewModel);
    }
}
