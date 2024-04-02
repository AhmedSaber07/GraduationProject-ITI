using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using E_Commerce.MVC.DTOs.UserAccount;
using Microsoft.AspNetCore.Identity;
using NuGet.Common;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

public class AdminController : Controller
{
    private readonly HttpClient _httpClient;

    public AdminController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://2bstore.somee.com/");
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
  
    

    public ActionResult AddAddress()
    {
        return View();
    }
    //Zara@123423
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddAddress(AddressDto addressDto)
    {

        addressDto.Email= HttpContext.Session.GetString("Email");
        
        string token = HttpContext.Session.GetString("AuthToken");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var jsonContent = JsonSerializer.Serialize(addressDto);
        var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
       
        var response = await _httpClient.PostAsync("api/UserAccount/AddAddress", stringContent);


        if (response.IsSuccessStatusCode)
        {

            return RedirectToAction("CategoryList", "Category");
        }
        else
        {
            return View("Error404");
        }
    }

    [HttpPut("{oldEmail}/UpdateEmail")]
    public async Task<IActionResult> UpdateEmail(string oldEmail, string newEmail)
    {
        var response = await _httpClient.PutAsync($"api/UserAccount/{oldEmail}/UpdateEmail?newEmail={newEmail}", null);

        return await HandleResponse(response);
    }

    [HttpPut("{oldPhone}/UpdatePhone")]
    public async Task<IActionResult> UpdatePhone(string oldPhone, string newPhone)
    {
        var response = await _httpClient.PutAsync($"api/UserAccount/{oldPhone}/UpdatePhone?newPhone={newPhone}", null);

        return await HandleResponse(response);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        var response = await _httpClient.PostAsync("api/UserAccount/Logout", null);

        return await HandleResponse(response);
    }  
    public ActionResult Login()
    {
        return View();
    }
    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginDto loginDtoo)
    {
        var jsonContent = JsonSerializer.Serialize(loginDtoo);
        var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/UserAccount/login", stringContent);
        string responseContent = await response.Content.ReadAsStringAsync();
        var LoginDto = JsonSerializer.Deserialize<LoginResponse>(responseContent);
        
        if (response.IsSuccessStatusCode)
        {

           // string hisName = (LoginDto.userDate.FirstName + " " + LoginDto.userDate.LastName);
          //  HttpContext.Session.SetString("AdminName", hisName);
            HttpContext.Session.SetString("AuthToken", LoginDto.token);
            HttpContext.Session.SetString("Email", loginDtoo.UserName);
            //TempData["AdminName"] = hisName;
            return RedirectToAction("CategoryList", "Category");
        }
        else 
        {
            ViewBag.ErrorMessage = "Wrong UserName Or password";
           return View();
        }
        
    }
    [HttpGet("reset-password")]
    public async Task<IActionResult> ResetPassword(string email, string token)
    {
        var response = await _httpClient.GetAsync($"api/UserAccount/reset-password?email={email}&token={token}");

        return await HandleResponse(response);
    }
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        var jsonContent = JsonSerializer.Serialize(resetPasswordDto);
        var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/UserAccount/reset-password", stringContent);

        return await HandleResponse(response);
    }

    [HttpPost("forget-password")]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        var response = await _httpClient.PostAsync($"api/UserAccount/forget-password?email={email}", null);

        return await HandleResponse(response);
    }

    private async Task<IActionResult> HandleResponse(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            
            return RedirectToAction("Index"); 
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
        
            return View("Unauthorized"); 
        }
        else
        {
            
            var errorMessage = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, errorMessage);
            return View(); 
        }
    }
}
