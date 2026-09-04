namespace LibraryManagement.Models
{
    public abstract class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }
        public string? Phone { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        protected Member( string name,string password,string email)
        {
         
            this.Name = name;
            this.Password = password;
            this.Email= email;  
        }

        public abstract int borrowLimit();
        public abstract int FineValue();
        public abstract int maxBorrowDuration();


    }
}
