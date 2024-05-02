using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PRN221_haicthe163677_FinalProject.Pages.Auth
{
    public class LogOutModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            HttpContext.Session.Remove("user");
            HttpContext.Session.Remove("token");
            return RedirectToPage("/Auth/Login");
        }
    }
}
