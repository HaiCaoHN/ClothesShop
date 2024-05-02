using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using BusinessObject.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PRN221_haicthe163677_FinalProject.Pages.Dashboard
{
    public class CreateModel : PageModel
    {
        private readonly ProjectPrnContext _context;
        private readonly HttpClient _httpClient = null;
        private string ApiUrl = "";
        public CreateModel(ProjectPrnContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "http://localhost:5070/api/";
        }

        public IActionResult OnGet()
        {
            string token = HttpContext.Session.GetString("token");
            if(token == null)
            {
                return RedirectToPage("./Index");
            }
            ViewData["CreateState"] = "";
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        [Display(Name = "Choose file to upload")]
        [BindProperty]
        [Required(ErrorMessage = "Please select a file.")]
        public IFormFile? FilesUpload { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            string token = HttpContext.Session.GetString("token");
            token = token.Replace("\"", "");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            if (_context.Products.FirstOrDefault(p=> p.ProductName == Product.ProductName) != null)
            {
                ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
                ViewData["CreateState"] = "Product existed";
                return Page();
            };
            if (FilesUpload != null)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + FilesUpload.FileName;
                string savePath = Path.Combine(@".\wwwroot\img\product", uniqueFileName);
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    FilesUpload.CopyTo(stream);
                }
                Product.Image = uniqueFileName;
            }
            if (_context.Products == null || Product == null)
            {
                return Page();
            }
            ViewData["CreateState"] = "";

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(ApiUrl + "product", Product);


            return RedirectToPage("./Index");
        }
    }
}
