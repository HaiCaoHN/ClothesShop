using BusinessObject.DataAccess;
using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Net.Http.Headers;
using System.Text.Json;


namespace PRN221_haicthe163677_FinalProject.Pages.Auth
{
    public class LoginModel : PageModel
    {

        private readonly HttpClient _httpClient = null;
        private string ApiUrl = "";

        [BindProperty]
        public LoginDto Account { get; set; } = default!;
        public LoginModel()
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "http://localhost:5070/api/";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Account == null)
            {
                return Page();
            }
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(ApiUrl + "auth/login", Account );
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                LoginDto info = JsonSerializer.Deserialize<LoginDto>(data, options);
                HttpContext.Session.SetString("user", JsonSerializer.Serialize(info.User));
                HttpContext.Session.SetString("token", JsonSerializer.Serialize(info.Token));
                ViewData["LoginState"] = "";
                return RedirectToPage("/Shop/Index");
            }
            else
            {
                ViewData["LoginState"] = "Wrong username/ Password";
            }
            return Page();
        }
        public IActionResult OnLogOut()
        {
            HttpContext.Session.Remove("user");
            return RedirectToPage("/Login/Index");
        }
    }
}
