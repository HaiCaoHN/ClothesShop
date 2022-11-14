using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Extensions;
namespace Project.Controllers
{
    public class LoginController : Controller
    {

        [HttpPost]
        public IActionResult Login(string name, string pass)
        {
            using (Project_PRNContext context = new Project_PRNContext())
            {
                var user = context.Users
                    .FirstOrDefault(u => u.UserName.Equals(name) && u.Password.Equals(pass));
                if (user == null)
                {
                    ViewBag.MessSignIn = "Wrong username or password!";
                    return View();
                }
                else
                {
                    HttpContext.Session.Set("user", user);
                    return RedirectToAction("Index","Home");
                }

            }

        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult SignUp(string name, string pass, string repass, string dname)
        {
            using (Project_PRNContext context = new Project_PRNContext())
            {
                var user = context.Users
                    .FirstOrDefault(u => u.UserName.Equals(name));
                if (user == null)
                {
                    if (!pass.Equals(repass))
                    {
                        ViewBag.MessSignUp = "Re-password not match origin password!";
                        return View("Login");
                    }
                    User u = new User()
                    {
                        UserName = name,
                        Password = pass,
                        DisplayName = dname
                    };
                    context.Users.Add(u);
                    context.SaveChanges();
                }
                else
                {
                    ViewBag.MessSignUp = "Username existed!";
                    return View("Login");
                }
                ViewBag.MessSignUp = "SignUp Successfully!";
                return View("Login");
            }
        }
    }
}
