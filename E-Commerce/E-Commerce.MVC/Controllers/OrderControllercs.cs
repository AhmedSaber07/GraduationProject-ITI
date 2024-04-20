using E_Commerce.MVC.DTOs.listResultDto;
using E_Commerce.MVC.DTOs.OrderDto;
using E_Commerce.MVC.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                var x = Order.entities.OrderBy(o => o.orderNumber);
               
                return View(x);
            }
            else
            {
                return View("Forbidden");
            }
        }
        public IActionResult Update(int id , string orderState)
        {
            string x = id.ToString();

			var enumValues = Enum.GetValues(typeof(OrderStateEn))
                                 .Cast<OrderStateEn>()
                                 .Select(state => new SelectListItem
                                 {
                                     Text = state.ToString(),
                                     Value = ((int)state).ToString(),
									 Selected = (((int)state) == id)
								 })
                                 .ToList();
            foreach (var item in enumValues)
            {
                if (item.Text == orderState) item.Selected = true;

            }
            ViewBag.StateList = enumValues;
            ViewBag.OrderNumber = id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(int orderNumber, int selectedState)
        {
            string token = HttpContext.Session.GetString("AuthToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PutAsync($"api/Order/ChangeOrderState/{orderNumber}/{selectedState}",null);
            if (response.IsSuccessStatusCode)
            {
                //ViewBag.response = "Updated successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Forbidden");
            }
        }
    }
}
