using BusinessObject.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace KarmaShop.Pages.Shop
{
    public class DetailModel : PageModel
    {
		private readonly int pageSize = 6;
		private readonly HttpClient _httpClient = null;
		private string ApiUrl = "";
        public DetailModel()
        {
			_httpClient = new HttpClient();
			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
			_httpClient.DefaultRequestHeaders.Accept.Add(contentType);
			ApiUrl = "http://localhost:5070/api/";
		}
		[BindProperty]
        public Product ProductDetail { get; set; }
        public async Task<ActionResult> OnGetAsync(int? prdId)
        {
			if(prdId == null)
			{
				return RedirectToPage("/Shop/Index");
			}
			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};
            string token = HttpContext.Session.GetString("token");
            token = token.Replace("\"", "");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = await _httpClient.GetAsync(ApiUrl + "product/" + prdId);
			if(response.IsSuccessStatusCode)
			{
				string data = await response.Content.ReadAsStringAsync();
				ProductDetail = JsonSerializer.Deserialize<Product>(data, options);
			}
			return Page();
		}
	}
}
