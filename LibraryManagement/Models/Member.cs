namespace LibraryManagement.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }
        public string? Phone { get; set; }
        public DateTime RegistrationDate { get; set; }

        public Member( string name,string password,string email)
        {
         
            this.Name = name;
            this.Password = password;
            this.Email= email;  
        }
    }
}
