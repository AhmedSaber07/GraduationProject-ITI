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
using Microsoft.AspNetCore.Hosting;
using E_Commerce.MVC.DTOs.ProductImageDto;
using System.IO;
using Humanizer;

namespace E_Commerce.MVC.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
     

        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IWebHostEnvironment webHostEnvironment)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://2bstore.somee.com/");
            _webHostEnvironment = webHostEnvironment;
        }
        public   async Task <IActionResult> ProductList()
        {
      
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
        public async Task<IActionResult> DeleteProduct(Guid Id)
        {
           
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Product/SoftDelete/{Id}");

            if (response.IsSuccessStatusCode)
            {

                var responseData = await response.Content.ReadAsStringAsync();
                return RedirectToAction("ProductList");

            }
            else
            {
                return View("ProductList", "Error: " + response.StatusCode);
            }
        }
        public async Task<IActionResult> DeleteProductImage(string url,Guid id)
        {



            //if (response.IsSuccessStatusCode)
            //{
            var imagename = url.Split("/").LastOrDefault();
            string imagePath = Path.Combine("https://2b-admin.somee.com/ProductsImages/");
            if (System.IO.File.Exists(imagePath))
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Product/ProductImage?url={url}");
                System.IO.File.Delete(imagePath + imagename);
                return View("Update", new { id = id });
            }
            else
            {
                return View("Update", new { id = id });
            }
            //}
            //else
            //{
            //    return View("Update", "Error: " + response.StatusCode);
            //}
        }
        public async Task<IActionResult> Update(Guid id)
        {
            var apiUrl = $"api/Product/{id}";
            var response = await _httpClient.GetAsync(apiUrl);
            var productData = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<resultDto<getProductwithImage>>(productData);


            string token = HttpContext.Session.GetString("AuthToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var apiUrl2 = $"api/Category/getAlldropdown";
            var response2 = await _httpClient.GetAsync(apiUrl2);
            var categorylistdata = await response2.Content.ReadAsStringAsync();
            var categoryList = JsonConvert.DeserializeObject<List<CategoryList>>(categorylistdata);
            ViewBag.Categories = categoryList;
            ///////////////////brand////////////
            var apiUrl3 = $"api/brand/getAlldropdown";
            var response3 = await _httpClient.GetAsync(apiUrl3);
            var brandslistdata = await response3.Content.ReadAsStringAsync();
            var brandsList = JsonConvert.DeserializeObject<List<CategoryList>>(brandslistdata);
            ViewBag.brands = brandsList;


            return View(product.Entity);
        }
        [HttpPost]
        public async Task<IActionResult> Update(getProductwithImage displayProductDto, Guid id)
        {

            createDto updatedProduct = new createDto()
            {
                nameAr = displayProductDto.nameAr,
                nameEn = displayProductDto.nameEn,
                descriptionAr = displayProductDto.descriptionAr,
                descriptionEn = displayProductDto.descriptionEn,
                colorAr = displayProductDto.colorAr,
                colorEn = displayProductDto.colorEn,
                price = displayProductDto.price,
                stockQuantity = displayProductDto.stockQuantity,
                categoryId = displayProductDto.categoryId,
                brandId = displayProductDto.brandId,
                FormFiles = displayProductDto.FormFiles
            };
            List<CreateWithProductDto> displayimages = new List<CreateWithProductDto>();

            foreach (var image in displayProductDto.images)
            {
                CreateWithProductDto imagedto = new CreateWithProductDto()
                {
                    imageUrl = image,
                };
                displayimages.Add(imagedto);
            }
            updatedProduct.Images= displayimages;


            try
            {
                if (updatedProduct.FormFiles != null)
                {
                    updatedProduct.Images = await SaveImages(updatedProduct.FormFiles);
                }
                var jsonContent = JsonSerializer.Serialize(updatedProduct);
                var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/Product/{id}/Product", stringContent);
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.ProductAdded = true;
                    return RedirectToAction("ProductList");
                }

                else
                {
                    ViewBag.ProductAdded = false;
                    ViewBag.ErrorMessage = "Product not added";

                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error404");
            }
        }
        public async Task<IActionResult> Add()
        {
            string token = HttpContext.Session.GetString("AuthToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var apiUrl2 = $"api/Category/getAlldropdown";
            var response2 = await _httpClient.GetAsync(apiUrl2);
            var categorylistdata = await response2.Content.ReadAsStringAsync();
            var categoryList = JsonConvert.DeserializeObject<List<CategoryList>>(categorylistdata);
            ViewBag.Categories = new SelectList(categoryList, "id", "name");
            ///////////////////brand////////////
             apiUrl2 = $"api/brand/getAlldropdown";
             response2 = await _httpClient.GetAsync(apiUrl2);
            var brandslistdata = await response2.Content.ReadAsStringAsync();
           var  brandsList = JsonConvert.DeserializeObject<List<CategoryList>>(brandslistdata);
            ViewBag.brands = new SelectList(brandsList, "id", "name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(createDto dto)
        {
            try
            {

                dto.Images = await SaveImages(dto.FormFiles);
                var jsonContent = JsonSerializer.Serialize(dto);
                var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Product", stringContent);
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.ProductAdded = true;
                    return View();
                }

                else
                {
                    ViewBag.ProductAdded = false;
                    ViewBag.ErrorMessage = "Product not added";

                    return View();
                }
                //if (response.IsSuccessStatusCode)
                //{


                //   // Console.WriteLine("DTO added successfully.");

                //    //var responseData = await response.Content.ReadAsStringAsync();
                //    //var dtoList = JsonSerializer.Deserialize<createDto>(responseData);
                //    //return RedirectToAction("ProductList");
                //}
                //else
                //{
                //    return RedirectToAction("Index", "Home");
                //}
            }
            catch (Exception ex)
            {
                return View("Error404");
            }
        }
        private async Task<List<CreateWithProductDto>> SaveImages(List<IFormFile> images)
        {
            var imagePaths = new List<CreateWithProductDto>();

            foreach (var image in images)
            {
                if (image != null && image.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    var filePath = Path.Combine("wwwroot/ProductsImages", fileName);
                    //var filePath = Path.Combine(directoryPath, fileName);
                    
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    CreateWithProductDto pushPath = new CreateWithProductDto();
                    pushPath.imageUrl = "https://2b-admin.somee.com/ProductsImages/" + fileName;
                    imagePaths.Add(pushPath);
                }
            }

            return imagePaths;
        }

    }
}
 

