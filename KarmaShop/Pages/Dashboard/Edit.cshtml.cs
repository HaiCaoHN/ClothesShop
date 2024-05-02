using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using BusinessObject.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KarmaShop.Utils;

namespace PRN221_haicthe163677_FinalProject.Pages.Dashboard
{
    public class EditModel : PageModel
    {
        private readonly ProjectPrnContext _context;
        private readonly HttpClient _httpClient = null;
        private string ApiUrl = "";
        public EditModel(ProjectPrnContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "http://localhost:5070/api/";
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        [Display(Name = "Choose file to upload")]
        [MaxFileSize(1 * 1024 * 1024)]
        [BindProperty]
        public IFormFile? FilesUpload { get; set; }
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

            var product = await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var userJson = HttpContext.Session.GetString("user");
            string token = HttpContext.Session.GetString("token");
            token = token.Replace("\"", "");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            if (FilesUpload != null)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + FilesUpload.FileName;
                string savePath = Path.Combine(@".\wwwroot\img\product\", uniqueFileName);
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    FilesUpload.CopyTo(stream);
                }
                if (Product.Image != null)
                {
                    string oldPath = Path.Combine(@".\wwwroot\img\product\", Product.Image);

                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                Product.Image = uniqueFileName;
            }
            else
            {
                string oldImage = _context.Products.AsNoTracking().FirstOrDefault(x => x.ProductId == Product.ProductId).Image;
                Product.Image = oldImage;
            }

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync(ApiUrl + "product", Product);

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["EditState"] = "Edit successfully!";
            return Page();
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
