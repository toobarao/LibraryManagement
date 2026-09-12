using LibraryManagement.Models;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Services
{
    public class AccountServices(ApplicationDbContext dbcontext):IAccountService
    {

        public bool Login(LoginViewModel loginViewModel)
        {
            if (loginViewModel.UserType == UserType.Librarian && (dbcontext.Librarian.Any(x => x.Name == loginViewModel.Name)))
            {
                var librarian = dbcontext.Librarian.FirstOrDefault(x => x.Name == loginViewModel.Name);
                if (librarian != null)
                {
                    var passwordHasher = new PasswordHasher<Librarian>();
                    var result = passwordHasher.VerifyHashedPassword(librarian, librarian.Password, loginViewModel.Password);
                    if (result == PasswordVerificationResult.Success)
                        return true;
                    else
                        return false;
                }
            }
            else if ((dbcontext.Memebers.Any(x => x.Name == loginViewModel.Name)))
            {

                Member member = dbcontext.Memebers.FirstOrDefault(x => x.Name == loginViewModel.Name);
                if (member != null)
                {
                    var passwordHasher = new PasswordHasher<Member>();
                    var result = passwordHasher.VerifyHashedPassword(member, member.Password, loginViewModel.Password);
                    if (result == PasswordVerificationResult.Success)
                        return true;
                    else
                        return false;
                }
            }
                
            return false;
        }


     
        public bool Register(RegisterViewModel registerViewModel) {
            if (registerViewModel.UserType == UserType.Librarian && !(dbcontext.Librarian.Any(x => x.Name == registerViewModel.Name)))
            {
                Librarian librarian = new(registerViewModel.Name, registerViewModel.Email);
                var hashPassword=new PasswordHasher<Librarian>();
                librarian.Password = hashPassword.HashPassword(librarian, registerViewModel.Password);
                dbcontext.Librarian.Add(librarian);
                dbcontext.SaveChanges();
                return true;
            }
            else if (registerViewModel.UserType == UserType.StudentMember && !(dbcontext.Memebers.Any(x => x.Name == registerViewModel.Name)))
            {

                StudentMember student = new(registerViewModel.Name, registerViewModel.Email);
                var hashPassword = new PasswordHasher<StudentMember>();
                student.Password = hashPassword.HashPassword(student, registerViewModel.Password);
                dbcontext.Memebers.Add(student);
                dbcontext.SaveChanges();
                return true; 
            }
            else if (registerViewModel.UserType == UserType.PremiumMember && !(dbcontext.Memebers.Any(x => x.Name == registerViewModel.Name)))
            {

                PremiumMember premium = new(registerViewModel.Name, registerViewModel.Email);
                var hashPassword = new PasswordHasher<PremiumMember>();
                premium.Password = hashPassword.HashPassword(premium, registerViewModel.Password);
              
                dbcontext.Memebers.Add(premium);
                dbcontext.SaveChanges();
                return true; 
            }
            return false;
        
        }


    }
}
