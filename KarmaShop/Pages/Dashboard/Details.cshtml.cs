﻿using System;
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
    public class DetailsModel : PageModel
    {
        private readonly ProjectPrnContext _context;
        private readonly HttpClient _httpClient = null;
        private string ApiUrl = "";
        public DetailsModel(ProjectPrnContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "http://localhost:5070/api/";
        }

        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            string token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return RedirectToPage("./Index");
            }

            token = token.Replace("\"", "");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            HttpResponseMessage response = await _httpClient.GetAsync(ApiUrl + "product/" + id);
            if(response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Product = JsonSerializer.Deserialize<Product>(data, options);
            }
            return Page();
        }
    }
}
