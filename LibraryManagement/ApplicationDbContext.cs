using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Borrowing> Borrowing{ get; set; }
        public DbSet<Member> Memebers { get; set; }
        public DbSet<Librarian> Librarian { get; set; }
        public DbSet<ReturnBook> ReturnBooks { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
       
        public DbSet<BorrowStatus> BorrowStatus { get; set; }
        public DbSet<RequestStatus> RequestStatus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
           .Property(x => x.Availability)
           .HasDefaultValue("Yes");
            modelBuilder.Entity<StudentMember>();
            modelBuilder.Entity<PremiumMember>();
        }
    }
}
