using Microsoft.AspNetCore.Mvc;
using E_Commerce.MVC.DTOs.productDto;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using E_Commerce.MVC.DTOs.OrderDto;
using NuGet.Protocol;
using E_Commerce.MVC.DTOs.listResultDto;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using E_Commerce.MVC.DTOs.UserAccount;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commerce.MVC.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        //public async Task<IActionResult> ProductsList()
        //{
        //    return View("Forbidden");
           
        //}
        //public async Task<IActionResult> UpdateProduct()
        //{
        //    return View();
        //}
        //public async Task<IActionResult> Details()
        //{
        //    return View();
        //}
        //public async Task<IActionResult> CreateProduct()
        //{
        //    return View();
        //}


        private readonly HttpClient _httpClient;

        public ProductController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://2bstore.somee.com/");
        }
        public   async Task <IActionResult> ProductList()
        {
            //return View("Forbidden");
            //return View();


            HttpResponseMessage response = await _httpClient.GetAsync("api/Product/getall");

            if (response.IsSuccessStatusCode)
            {

                var responseData = await response.Content.ReadAsStringAsync();
                var dtoList = JsonSerializer.Deserialize<List<GetProductDto>>(responseData);
                return View(dtoList);
            }
            else
            {
                return View("Forbidden");
            }

        }
        public async Task<IActionResult> Details(Guid id)
        {
            //return View("Forbidden");
            //return View();


            HttpResponseMessage response = await _httpClient.GetAsync($"api/Product/{id}");

            if (response.IsSuccessStatusCode)
            {

                var responseData = await response.Content.ReadAsStringAsync();  
                var dtoList = JsonConvert.DeserializeObject<resultDto<getProductwithImage>>(responseData);
                return View(dtoList.Entity);
            }
            else
            {
                return View("Forbidden");
            }

        }
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
           
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Product/SoftDelete/{id}");

            if (response.IsSuccessStatusCode)
            {

                var responseData = await response.Content.ReadAsStringAsync();
                return View("ProductList", responseData);

            }
            else
            {
                return View("ProductList", "Error: " + response.StatusCode);
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
        public async Task<IActionResult> Add()
        {
            var apiUrl2 = $"api/Category/getAlldropdown";
            var response2 = await _httpClient.GetAsync(apiUrl2);
            var categorylistdata = await response2.Content.ReadAsStringAsync();
            var categoryList = JsonConvert.DeserializeObject<List<CategoryList>>(categorylistdata);
            ViewBag.Categories = new SelectList(categoryList, "id", "name");
            return View();
        }
        [HttpPost]
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
                    var dtoList = JsonSerializer.Deserialize<createDto>(responseData);
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
 

