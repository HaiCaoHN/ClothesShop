using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_haicthe163677_FinalProject.Utils;

namespace PRN221_haicthe163677_FinalProject.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly ProjectPrnContext _context;

        private readonly int pageSize = 4;

        public IndexModel(ProjectPrnContext context)
        {
            _context = context;
        }
        public string currentSearch { get; set; }
        public PaginatedList<Product> Product { get; set; } = default!;

        public async Task<ActionResult> OnGetAsync(string searchString, int? pageIndex, string currentFilter)
        {
            string token = HttpContext.Session.GetString("token");
            ViewData["EditState"] = "";

            if (token != null)
            {
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
                Product = await PaginatedList<Product>.CreateAsync(products.AsNoTracking(), pageIndex ?? 1, pageSize);
                return Page();
            }
            return RedirectToPage("/Shop/Index");

        }
    }
}
