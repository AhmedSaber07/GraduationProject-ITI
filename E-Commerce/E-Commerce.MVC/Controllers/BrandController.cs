using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using E_Commerce.MVC.DTOs.BrandDto;
using System.Text.Json;
using E_Commerce.MVC.DTOs.listResultDto;
using Microsoft.AspNetCore.Authorization;
namespace E_Commerce.MVC.Controllers
{
    [Authorize]
    public class BrandController : Controller
    {

        public async Task<IActionResult> BransList()
        {
            return View();
        }
        public async Task<IActionResult> CreateBrand()
        {
            return View();
        }
        public async Task<IActionResult> UpdateBrand()
        {
            return View();
        }
        private readonly HttpClient _httpClient;
        public BrandController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://twobwebstie-001-site1.ftempurl.com");
        }
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/brand");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var brands = JsonSerializer.Deserialize<listResultDto<GetBrandDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(brands);
            }
            else
            {
                return View("Error404");
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDto brand)
        {
            var jsonContent = JsonSerializer.Serialize(brand);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/brand", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("Error404");
            }
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/brand/SoftDelete/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("Error");
            }
        }
    }
}
