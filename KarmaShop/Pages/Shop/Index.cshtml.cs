
using BusinessObject.DataAccess;
using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_haicthe163677_FinalProject.Utils;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text.Json;

namespace PRN221_haicthe163677_FinalProject.Pages.Shop
{
    public class IndexModel : PageModel
    {
        private readonly ProjectPrnContext _context;
        private readonly int pageSize = 6;
        private readonly HttpClient _httpClient = null;
        private string ApiUrl = "";
        public IndexModel(ProjectPrnContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "http://localhost:5070/api/";
        }

        [BindProperty]
        public PaginatedList<Product> Products { get; set; }

        [BindProperty]
        public List<Category> Categories { get; set; }

        public string currentSearch { get; set; }


        public async Task OnGetAsync(string searchString, int? pageIndex, string currentFilter, int? cateId)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            //call api get cate
            HttpResponseMessage responseCate = await _httpClient.GetAsync(ApiUrl + "product/category");
            if (responseCate.IsSuccessStatusCode)
            {
                string dataCate = await responseCate.Content.ReadAsStringAsync();
                Categories = JsonSerializer.Deserialize<List<Category>>(dataCate, options);
            }
            IQueryable<Product> products = _context.Products.Include(x => x.Category);
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            currentSearch = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.ProductName.Contains(searchString)).Include(x => x.Category);
            }

            if (cateId != null)
            {
                products = products.Where(p => p.CategoryId == cateId);
            }
            Products = await PaginatedList<Product>.CreateAsync(products.AsNoTracking(), pageIndex ?? 1, pageSize);
            if (HttpContext.Session.GetString("user") != null)
            {
                int quan = GetCart().CartItems.Sum(p => p.Quantity);
                HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(quan.ToString()));
            }

        }

        public List<CartItem> Cart()
        {
            var dataJson = HttpContext.Session.GetString("Cart");
            List<CartItem> cart = null;
            if (dataJson == null)
            {
                cart = new List<CartItem>();
            }
            else
            {
                cart = JsonSerializer.Deserialize<List<CartItem>>(dataJson);
            }
            return cart;
        }

        public Cart GetCart()
        {
            var userJson = HttpContext.Session.GetString("user");
            var user = JsonSerializer.Deserialize<User>(userJson);
            Cart cart = _context.Carts.Include(cart => cart.CartItems).FirstOrDefault(cart => cart.UserId == user.UserId);
            return cart ?? new BusinessObject.DataAccess.Cart();
        }

        public async Task<IActionResult> OnGetCart(int? id, string? type, int? amount)
        {
            int total = (amount == null) ? 1 : (int)amount;
            var cart = GetCart();
            var userJson = HttpContext.Session.GetString("user");
            var user = JsonSerializer.Deserialize<User>(userJson);

            var item = cart.CartItems.FirstOrDefault(p => p.ProductId == id);
            Product p = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (item == null)
            {
                item = new CartItem
                {
                    Quantity = total,
                    Product = p
                };
                cart.CartItems.Add(item);
            }
            else
            {
                item.Quantity += total;
            }

            _context.Entry<Cart>(cart).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            int quantity = cart.CartItems.Sum(p => p.Quantity);
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(quantity.ToString()));
            if (type == "ajax")
            {
                return new JsonResult(quantity);
            }
            return RedirectToPage("./Index");
        }


    }
}

