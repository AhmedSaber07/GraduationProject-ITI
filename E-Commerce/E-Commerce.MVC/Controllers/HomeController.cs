
using E_Commerce.MVC.DTOs.listResultDto;
using E_Commerce.MVC.DTOs.OrderDto;
using E_Commerce.MVC.DTOs.UserAccount;
using E_Commerce.MVC.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text.Json;

namespace E_Commerce.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly HttpClient _httpClient;
        public HomeController()
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
            //https://2bstore.somee.com/api/UserAccount/GetUsersData
            var response2 = await _httpClient.GetAsync("api/UserAccount/GetUsersData");
            if (response2.IsSuccessStatusCode)
            {
                var content = await response2.Content.ReadAsStringAsync();
                var users = JsonSerializer.Deserialize<List<GetUsersData>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                ViewBag.Users = users.Count;
            }
                var response = await _httpClient.GetAsync("api/Order");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Order = JsonSerializer.Deserialize<listResultDto<getOrdersWithoutItems>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                List<int> OrderNumber = Enumerable.Repeat(0, 5).ToList();
                foreach (var item in Order.entities)
                {
                    if (item.status_en == "Pending")
                        OrderNumber[0]++;
                    if (item.status_en == "Confirmed")
                        OrderNumber[1]++;
                    if (item.status_en == "Canceled")
                        OrderNumber[2]++;
                    if (item.status_en == "Received")
                        OrderNumber[3]++;
                    if (item.status_en == "Attempted_delivery")
                        OrderNumber[4]++;
                }
                return View(OrderNumber);
            }
            else
            {
                return View("Forbidden");
            }
        }
    }
}
