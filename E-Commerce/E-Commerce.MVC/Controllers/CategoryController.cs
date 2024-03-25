using E_Commerce.MVC.DTOs.CategoryDto;
using E_Commerce.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace E_Commerce.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly HttpClient _httpClient;


       public CategoryController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://twobwebstie-001-site1.ftempurl.com");
        }
        private void Auth()
        {
            string username = "11168799";
            string password = "60-dayfreetrial";
            string base64Auth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Auth);
        }
        public async Task<IActionResult> IndexAsync()
        {

           Auth();
            HttpResponseMessage response = await _httpClient.GetAsync("api/Category/Getall1");
            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                
                var responseData = await response.Content.ReadAsStringAsync();
                var dtoList = JsonSerializer.Deserialize<List<CreateOrUpdateCategoryDto>>(responseData); 
                return View(dtoList);
            }
            else
            {
             return View("Error: " + response.StatusCode);
            }
          
        }


    }
}
