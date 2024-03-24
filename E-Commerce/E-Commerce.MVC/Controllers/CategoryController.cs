using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;

namespace E_Commerce.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly HttpClient _httpClient;


       public CategoryController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://twobwebstie-001-site1.ftempurl.com");
        }

        public async Task<IActionResult> IndexAsync()
        {
            string username = "11168799";
            string password = "60-dayfreetrial";
            string base64Auth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Auth);
           
            HttpResponseMessage response = await _httpClient.GetAsync("api/Category/UpdatedGetall");


            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string responseData = await response.Content.ReadAsStringAsync();
                // Process responseData as needed
                return Content(responseData);
            }
            else
            {
                // Handle error response
                return Content("Error: " + response.StatusCode);
            }
          
        }
    }
}
