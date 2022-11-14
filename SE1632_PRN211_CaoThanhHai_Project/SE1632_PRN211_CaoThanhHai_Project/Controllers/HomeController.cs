using Microsoft.AspNetCore.Mvc;

namespace SE1632_PRN211_CaoThanhHai_Project.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
