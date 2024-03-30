using E_Commerce.MVC.DTOs.CategoryDto;
using E_Commerce.MVC.DTOs.listResultDto;
using E_Commerce.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            _httpClient.BaseAddress = new Uri("https://2bstore.somee.com/");
       }
        public async Task<IActionResult> Index()
        {


            HttpResponseMessage response = await _httpClient.GetAsync("api/Category/Getall1");

            if (response.IsSuccessStatusCode)
            {

                var responseData = await response.Content.ReadAsStringAsync();
                var dtoList = System.Text.Json.JsonSerializer.Deserialize<List<CreateOrUpdateCategoryDto>>(responseData);
                return View(dtoList);
            }
            else
            {
                return View("Error: " + response.StatusCode);
            }

        }
        public async Task<IActionResult> CategoryList()
        {

          
            HttpResponseMessage response = await _httpClient.GetAsync("api/Category/Getall1");
          
            if (response.IsSuccessStatusCode)
            {
                
                var responseData = await response.Content.ReadAsStringAsync();
                var dtoList = System.Text.Json.JsonSerializer.Deserialize<List<CreateOrUpdateCategoryDto>>(responseData); 
                return View(dtoList);
            }
            else
            {
             return View("Error: " + response.StatusCode);
            }
          
        }
        public async Task<IActionResult> Delete(Guid id)
        {
           
            string apiUrl = $"api/Category/SoftDelete/{id}";

            var request = new HttpRequestMessage(HttpMethod.Delete, apiUrl);

            using (var response = await _httpClient.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("CategoryList");
                }
                else 
                {
                    return View("Error404");
                }
              
            }
        }
        public async Task<IActionResult> Update(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var apiUrl = $"api/Category/{id}"; 
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var categoryData = await response.Content.ReadAsStringAsync();
                var category = JsonConvert.DeserializeObject<resultDto<CreateOrUpdateCategoryDto>>(categoryData); // Deserialize JSON response to your category model
                return View(category.Entity);
            }
          
            else
            {
                return View("Error404");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(CreateOrUpdateCategoryDto categoryDto)
        {
          
            try
            {
                
                var jsonContent = System.Text.Json.JsonSerializer.Serialize(categoryDto);
                var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

       
                HttpResponseMessage response;
                if (categoryDto == null)
                {
                    response = await _httpClient.PostAsync("api/Category", stringContent); 
                }
                else
                {
                    response = await _httpClient.PutAsync("api/Category", stringContent);
                }

             
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    return View("CategoryList", responseData);
                }
                else
                {
                    return View("Error404");
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync("Errror"+ex);
            }
            return View("CategoryList");
        }
        public IActionResult create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> create(CreateOrUpdateCategoryDto dto)
        {
            try
            {
                
                var jsonContent = System.Text.Json.JsonSerializer.Serialize(dto);
                var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Category", stringContent);

                if (response.IsSuccessStatusCode)
                {
                   
                    Console.WriteLine("DTO added successfully.");
                }
                else
                {
                   
                    Console.WriteLine($"Failed to add DTO. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {               
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return View();
        }



























    }
}
