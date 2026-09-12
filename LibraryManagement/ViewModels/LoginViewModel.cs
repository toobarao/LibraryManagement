using LibraryManagement.Models;

namespace LibraryManagement.ViewModels
{
    public class LoginViewModel
    {
        public required string Name { get; set; }
        public required string Password { get; set; }
        public required UserType UserType { get; set; }
    }
  

}
