using LibraryManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels
{
    public class RegisterViewModel
    {
        public required string Name { get; set; }
        public required string Password { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required UserType UserType { get; set; }
    }
}
