using E_Commerce.MVC.DTOs.listResultDto;
using E_Commerce.MVC.DTOs.OrderDto;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace E_Commerce.MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient _httpClient;
       public OrderController()
       {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://2bstore.somee.com/");
       }
        public async Task<IActionResult> Index()
        {
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
