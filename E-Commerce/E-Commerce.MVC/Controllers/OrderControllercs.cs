using E_Commerce.MVC.DTOs.listResultDto;
using E_Commerce.MVC.DTOs.OrderDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace E_Commerce.MVC.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly HttpClient _httpClient;
       public OrderController()
       {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://2bstore.somee.com/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           
        }
        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Session.GetString("AuthToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync("api/Order");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Order = JsonSerializer.Deserialize<listResultDto<getOrdersWithoutItems>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(Order.entities);
            }
            else
            {
                return View("Forbidden");
            }
        }

    }
}
