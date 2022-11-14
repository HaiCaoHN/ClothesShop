using Microsoft.AspNetCore.Mvc;
using Project.Models;

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
                    return RedirectToAction("Index");
                }

            }

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(string name, string pass, string repass)
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
                        return RedirectToAction("Index");
                    }
                    User u = new User()
                    {
                        UserName = name,
                        Password = pass
                    };
                    context.Users.Add(u);
                    context.SaveChanges();
                }
                else
                {
                    ViewBag.MessSignUp = "Username existed!";
                    return RedirectToAction("Index");
                }
                return View();
            }
        }
    }
}
