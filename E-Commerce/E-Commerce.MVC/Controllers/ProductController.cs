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
        public async Task<IActionResult> IndexAsync()
        {

           
            HttpResponseMessage response = await _httpClient.GetAsync("/getall");
           
            if (response.IsSuccessStatusCode)
            {

                var responseData = await response.Content.ReadAsStringAsync();
                var dtoList = JsonSerializer.Deserialize<List<GetProductDto>>(responseData);
                return View(dtoList);
            }
            else
            {
                return View("Error: " + response.StatusCode);
            }

        }
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
           
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Product/SoftDelete/{id}");

            if (response.IsSuccessStatusCode)
            {

                var responseData = await response.Content.ReadAsStringAsync();
                return View("Index", responseData);

            }
            else
            {
                return View("Index", "Error: " + response.StatusCode);
            }
        }
        public async Task<IActionResult> Update(updateDto ProductDto, Guid id)
        {
           
            try
            {

                var jsonContent = JsonSerializer.Serialize(ProductDto);
                var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");


                HttpResponseMessage response;
                if (id == Guid.Empty)
                {
                    response = await _httpClient.PostAsync($"api/Product/{id}", stringContent);
                }
                else
                {
                    response = await _httpClient.PutAsync($"api/Product/{id}", stringContent);
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
                await Console.Out.WriteLineAsync("Errror" + ex);
            }
            return View("Index");
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
 

