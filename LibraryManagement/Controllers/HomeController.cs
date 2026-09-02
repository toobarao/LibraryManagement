using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LibraryManagement.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _dbcontext;
       

        public HomeController(ApplicationDbContext dbcontext)
        {
        _dbcontext = dbcontext;
    }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            if (user.UserType == "Librarian")
            {
              var librarian=  _dbcontext.Librarian.Where(x => x.Name == user.Name && x.Password == user.Password);
               HttpContext.Session.SetString("Username", user.Name);
                if (librarian.Count()!=0)
                    return RedirectToAction("Index","Librarian");
            }
            else
            {
                var member = _dbcontext.Memebers.Where(x => x.Name == user.Name && x.Password == user.Password);
                HttpContext.Session.SetString("Username", user.Name);
                if (member.Count()!=0)
                    return RedirectToAction("Index", "Member");

            }
            return View("Index");

        }


       
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (user.UserType == "Librarian")
            {
                _dbcontext.Librarian.Add(new Librarian(user.Name,user.Password));
                _dbcontext.SaveChanges();
                 
                //var librarian = _dbcontext.Librarian.Add(x => x.Name == user.Name && x.Password == user.Password);
               
            }
            else
            {
                _dbcontext.Memebers.Add(new Member( user.Name, user.Password,user.Email));
                _dbcontext.SaveChanges();


            }
            return View("Index");
        }



        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
