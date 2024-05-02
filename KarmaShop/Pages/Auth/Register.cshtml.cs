using BusinessObject.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PRN221_haicthe163677_FinalProject.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly ProjectPrnContext _context;
        [BindProperty]
        public User Account { get; set; } = default!;

        [BindProperty]
        public string RePassword { get; set; }
        public RegisterModel(ProjectPrnContext contex)
        {
            _context = contex;
        }

        public async Task<IActionResult> OnPostAsync(string pass, string repass, string username, string fullname)
        {
            if (_context.Users.Any(x => x.UserName == username))
            {
                ViewData["RegisterState"] = "Username existed!";
                return Page();
            }

            if (!pass.Equals(repass))
            {
                ViewData["RegisterState"] = "Password and confirm password not match!";
                return Page();
            }
            User loggedAccount = new User
            {
                UserName = username,
                Password = pass,
                DisplayName = fullname,
                Role = 1
            };
            
            
            _context.Users.Add(loggedAccount);
            _context.SaveChanges();
            Cart cart = new Cart
            {
                UserId = loggedAccount.UserId
            };
            _context.Carts.Add(cart);
            _context.SaveChanges();
            ViewData["RegisterState"] = null;
            return RedirectToPage("/Auth/Login");
        }
    }
}
