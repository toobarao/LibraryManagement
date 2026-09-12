namespace LibraryManagement.ViewModels
{
    public class ProfileViewModel
    {
        
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string Phone { get; set; }
       
    }
    public class UpdatePasswordViewModel
    {
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }    
    }
}
