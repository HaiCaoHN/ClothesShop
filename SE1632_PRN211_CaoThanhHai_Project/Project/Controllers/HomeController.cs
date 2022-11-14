using Microsoft.AspNetCore.Mvc;
using Project.Models;
using System.Diagnostics;
using X.PagedList;
using System.Collections.Generic;
using Project.Extensions;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? page, int? cate,string? search)
        {
            using (Project_PRNContext context = new Project_PRNContext())
            {
                var cats = context.Categories.ToList();
                ViewBag.Categories = cats;
                //
                if (page == null) page = 1;
                if (search == null) search = "";
                int pageSize = 6;
                int pageNumber = (page ?? 1);
                var products = context.Products.ToList();
                cate = cate ?? 0;
                if(cate!=null && cate!=0)
                {
                    products = context.Products
                        .Where(p => p.CategoryId == cate && p.ProductName.Contains(search))
                        .ToList();
                }
                if (cate == 0)
                {
                    products = context.Products
                        .Where(p=> p.ProductName.Contains(search))
                        .ToList();
                }
                ViewBag.Cate = cate;
                //
                return View(products.ToPagedList(pageNumber,pageSize));
            }
            
        }

        public List<CartItem> Cart()
        {
            var data = HttpContext.Session.Get<List<CartItem>>("Cart");
            if(data == null)
            {
                data = new List<CartItem>();
            }
            return data;
        }

        public IActionResult AddCart(int? id)
        {
            var cart = Cart();
            var item = cart.FirstOrDefault(p=>p.ProductId == id);
            if(item == null)
            {
                item = new CartItem();
            }
            else
            {
                item.Quantity++;
            }
            HttpContext.Session.Set("Cart", cart);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}