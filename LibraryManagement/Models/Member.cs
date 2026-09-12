using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public abstract class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? Phone { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        protected Member(string name, string email)
        {
            this.Name = name;
            this.Email = email;
        }

        public abstract int borrowLimit();
        public abstract int FineValue();
        public abstract int maxBorrowDuration();


    }
}
