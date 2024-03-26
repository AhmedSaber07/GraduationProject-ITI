using Microsoft.AspNetCore.Mvc;
using E_Commerce.MVC.DTOs.productDto;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace E_Commerce.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://2b.somee.com");
        }
  
     

        public async Task<IActionResult> Add(createDto dto)
        {
            try
            {

                var jsonContent = JsonSerializer.Serialize(dto);
                var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Product", stringContent);

                if (response.IsSuccessStatusCode)
                {

                    Console.WriteLine("DTO added successfully.");
                    var responseData = await response.Content.ReadAsStringAsync();
                    var dtoList = JsonSerializer.Deserialize<List<createDto>>(responseData);
                    return View(dtoList);
                }
                else
                {

                    return View(dto);
                }
            }
            catch (Exception ex)
            {
                return View(dto);
            }
        }

    }
}
 

