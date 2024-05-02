using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessObject.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace PRN221_haicthe163677_FinalProject.Pages.Dashboard
{
    public class DeleteModel : PageModel
    {
        private readonly ProjectPrnContext _context;
        private readonly HttpClient _httpClient = null;
        private string ApiUrl = "";
        public DeleteModel(ProjectPrnContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "http://localhost:5070/api/";
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            string token = HttpContext.Session.GetString("token");

            if (token == null)
            {
                return RedirectToPage("./Index");
            }

            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Product = product;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            string token = HttpContext.Session.GetString("token");
            token = token.Replace("\"", "");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = await _httpClient.DeleteAsync(ApiUrl + "product/" + id);

            CartItem deletedItem = null!;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            
            return RedirectToPage("./Index");
        }
    }
}
