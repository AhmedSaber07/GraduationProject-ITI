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
        public async Task<IActionResult> CreateCategory()
        {
            return View();
        }
        public async Task<IActionResult> UpdateCategory()
        {
            return View();
        }
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
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            Auth();
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Category/SoftDelete/{id}");
           
            if (response.IsSuccessStatusCode)
            {

                var responseData = await response.Content.ReadAsStringAsync();
                return View("Index",responseData);
             
            }
            else
            {
                return View("Index", "Error: " + response.StatusCode);
            }
        }
        public async Task<IActionResult> Update(CreateOrUpdateCategoryDto categoryDto)
        {
            Auth();
            try
            {
                
                var jsonContent = JsonSerializer.Serialize(categoryDto);
                var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

       
                HttpResponseMessage response;
                if (categoryDto.id == Guid.Empty)
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
                    return View("Index", responseData);
                }
                else
                {
                    return View("Error: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync("Errror"+ex);
            }
            return View("Index");
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
                
                var jsonContent = JsonSerializer.Serialize(dto);
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
