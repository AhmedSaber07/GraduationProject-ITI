using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using E_Commerce.MVC.DTOs.UserAccount;
using Microsoft.AspNetCore.Identity;

public class AdminController : Controller
{
    private readonly HttpClient _httpClient;

    public AdminController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://2bstore.somee.com/"); 
    }
  
    [HttpPost]
    [ValidateAntiForgeryToken]

    public ActionResult AddAddress()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddAddress(AddressDto addressDto)
    {
        var jsonContent = JsonSerializer.Serialize(addressDto);
        var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/UserAccount/AddAddress", stringContent);

        return await HandleResponse(response);
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
    public async Task<ActionResult> Login(LoginDto loginDto)
    {
        var jsonContent = JsonSerializer.Serialize(loginDto);
        var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/UserAccount/login", stringContent);

        if (response.IsSuccessStatusCode)
        {

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
