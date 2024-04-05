﻿using Microsoft.AspNetCore.Mvc;
using System.Text;
using E_Commerce.MVC.DTOs.UserAccount;
using System.Net.Http.Headers;
using Newtonsoft.Json;
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

	public ActionResult ResetPassword()
	{
		return View();
	}
	
    [HttpPost("sendCode")]
	public async Task< ActionResult> sendCode(string email)
    {
        HttpContext.Session.SetString("resEmail", email);
        var apiUrl = $"api/UserAccount/SendCode?email={Uri.EscapeDataString(email)}";

		HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, null);

		if (response.IsSuccessStatusCode)
		{
			return View("EnterCode");
		}
		else
		{
			return RedirectToAction("ResetPassword");
		}

	}
    public async Task<ActionResult> EnterCode( )
    {
        return View();
    }
    [HttpPost("EnterCode")]
    public async Task<ActionResult> EnterCode(string input1, string input2, string input3, string input4)
    {
        string email = HttpContext.Session.GetString("resEmail");
        string scode = input1 + input2 + input3 + input4;
        int code = int.Parse(scode);

        var apiUrl = $"/api/UserAccount/CheckCode?code={code}&email={email}";

        HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, null);


        if (response.IsSuccessStatusCode)
        {

            bool isSuccess = await response.Content.ReadFromJsonAsync<bool>();

            if (isSuccess)
            {
                return RedirectToAction("EnterNewPassword");
            }
            else
            {
                ViewBag.ErrorMessage = "Code Not Correct";
                return View("EnterCode");
            }
        }
        else
        {
            ViewBag.ErrorMessage = "Code Not Correct";
            return View("EnterCode");
        }



    }
    public  ActionResult EnterNewPassword()
	{
		return View();
	}
    [HttpPost("EnterNewPassword")]
    public async Task<ActionResult> EnterNewPassword(string Password)
    {
            var apiUrl = "api/UserAccount/NewResetPassword";
        string email = HttpContext.Session.GetString("resEmail");
            var requestContent = new StringContent(JsonConvert.SerializeObject(new
            {
                Email = email,
                NewPassword = Password
            }), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, requestContent);

            if (response.IsSuccessStatusCode)
            {
            return RedirectToAction("Login", "Admin");
             }
            else
            {
              return View();

            }
        
    }
    public async Task<IActionResult> AddAddress()
    {
        string em = HttpContext.Session.GetString("Email");
        if (em ==null)
        {
            return View("Error404");
        }

            var apiUrl = $"api/UserAccount/GetUserAddress/?email={em}";
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var UserData = await response.Content.ReadAsStringAsync();
                var UserData2 = JsonConvert.DeserializeObject<AddressDto>(UserData);
                return View(UserData2);
            }

            else
            {
                return View("Error404");
            }
        
       
    }
    //Zara@123423
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddAddress(AddressDto addressDto)
    {

        addressDto.Email= HttpContext.Session.GetString("Email");
        
        string token = HttpContext.Session.GetString("AuthToken");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var jsonContent = System.Text.Json.JsonSerializer.Serialize(addressDto);
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
    [HttpPut("{oldPhone}/UpdatePhone")]
    public async Task<IActionResult> UpdatePhone(string oldPhone, string newPhone)
    {
        var response = await _httpClient.PutAsync($"api/UserAccount/{oldPhone}/UpdatePhone?newPhone={newPhone}", null);

        return await HandleResponse(response);
    }   
    public async Task<IActionResult> Logout()
    {
        var response = await _httpClient.PostAsync("api/UserAccount/Logout", null);

       return RedirectToAction("Login", "Admin");
    }  
    public ActionResult Login()
    {
        return View();
    }
    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginDto loginDtoo)
    {
        var jsonContent = System.Text.Json.JsonSerializer.Serialize(loginDtoo);
        var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/UserAccount/login", stringContent);
        string responseContent = await response.Content.ReadAsStringAsync();
        var LoginDto = System.Text.Json.JsonSerializer.Deserialize<LoginResponse>(responseContent);
        
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
