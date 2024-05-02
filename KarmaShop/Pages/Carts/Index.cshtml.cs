using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text.Json;
using BusinessObject.DataAccess;
using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_haicthe163677_FinalProject.Utils;

namespace PRN221_haicthe163677_FinalProject.Pages.Carts
{
    public class IndexModel : PageModel
    {
        private readonly ProjectPrnContext _context;
        public IndexModel(ProjectPrnContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "http://localhost:5070/api/";
        }

        private readonly HttpClient _httpClient = null;
        private string ApiUrl = "";

        [BindProperty]
        public List<CartItem> UserCart { get; set; }
        public CartItem CartItem { get; set; }

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
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                cart = JsonSerializer.Deserialize<List<CartItem>>(dataJson, options);

            }
            return cart;
        }

        public async Task<Cart> GetCart()
        {
            var userJson = HttpContext.Session.GetString("user");
            string s = HttpContext.Session.GetString("token");
            s = s.Replace("\"", "");
            var user = JsonSerializer.Deserialize<User>(userJson);

            Cart cart = null!;

            //call api to get carts
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + s);
            HttpResponseMessage response = await _httpClient.GetAsync(ApiUrl + "cart/" + user.UserId);
            if(response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                cart  = JsonSerializer.Deserialize<Cart>(data, options);
            }

            return cart ?? new BusinessObject.DataAccess.Cart();
        }
        public IActionResult OnGet()
        {
            string token = HttpContext.Session.GetString("token");

			if (token != null)
            {
				UserCart = GetCart().Result.CartItems.ToList();
                return Page();
            }
			return RedirectToPage("/Shop/Index");
		}

        public async Task<IActionResult> OnGetChange(int id, int quantity, string type)
        {
            var cart = GetCart().Result;
            var item = cart.CartItems.FirstOrDefault(p => p.ProductId == id);
            var userJson = HttpContext.Session.GetString("user");
            var user = JsonSerializer.Deserialize<User>(userJson);

            Product p = _context.Products.FirstOrDefault(p => p.ProductId == id);
            Cart c = _context.Carts.FirstOrDefault(c => c.UserId == user.UserId);
            item.Quantity = quantity;
            double? total = 0;
            foreach (CartItem ca in cart.CartItems)
            {
                total += ca.Quantity * ca.Product.Price;
            }
            UserCart = cart.CartItems.ToList();

            _context.Entry<Cart>(cart).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            int quan = cart.CartItems.Sum(p => p.Quantity);
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(quan.ToString()));

            var resp = new
            {
                Quantity = UserCart.Sum(p => p.Quantity),
                ProductId = id,
                Total = total
            };
            if (type == "ajax")
            {
                return new JsonResult(resp);
            }
            return RedirectToPage("./Index");

        }

        public async Task<IActionResult> OnGetRemove(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);

            if (cartItem != null)
            {
                CartItem = cartItem;
                _context.CartItems.Remove(CartItem);
                await _context.SaveChangesAsync();
            }

            int quan = GetCart().Result.CartItems.Sum(p => p.Quantity);
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(quan.ToString()));

            return RedirectToPage("./Index");

        }
    }
}
