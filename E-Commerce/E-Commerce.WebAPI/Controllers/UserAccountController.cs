using Azure;
using Company.Dtos.ViewResult;
using E_Commerce.Application.Services;
using E_Commerce.Application.Settings;
using E_Commerce.Domain.DTOs.UserAccount;
using E_Commerce.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly SignInManager<MyUser> _SignInManager;
        private readonly UserManager<MyUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IEmailService _emailService;

        public UserAccountController(SignInManager<MyUser> signInManager, 
            UserManager<MyUser> userManager, IConfiguration configuration,
            RoleManager<IdentityRole<Guid>> roleManager
            , IEmailService emailService)
        {
            _SignInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _emailService = emailService;
        }
        //[HttpPost("Login")]
        //public async Task<IActionResult> Login(LoginDto loginDto)
        //{



        //}
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto,string role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exist = await _userManager.FindByEmailAsync(registerDto.Email);
            if (exist != null)
            {
                return StatusCode(500, "The email already exist");
            }
            exist = await _userManager.FindByNameAsync(registerDto.Phone);

            if (exist != null)
            {
                return StatusCode(500, "The Phone already exist");
            }
            /////
            MyUser user = new MyUser() { Email = registerDto.Email, UserName = registerDto.Phone ,SecurityStamp=Guid.NewGuid().ToString()};
            if (await _roleManager.RoleExistsAsync(role))
            {
                var result = await _userManager.CreateAsync(user, registerDto.Password);
               
                if (!result.Succeeded)
                {
                    return StatusCode(500, result);
                }
                else 
                {
                    await _userManager.AddToRoleAsync(user, "user");
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action(nameof(ConfirmEmail), "UserAccount", new { token, email = user.Email }, Request.Scheme);
                    var message = new Message(new string[] { user.Email }, "Confirmation email link", confirmationLink);
                    _emailService.SendEmail(message);
                    return Ok("Created Successfuly");
                }
            }         
            return StatusCode(500, "Error exist");

        }

        //[HttpGet]
        //public IActionResult test()
        //{
        //    var message = new Message(new string[] { "sarazayan988@gmail.com" }, "Your registration", "the email send successflly");
        //    _emailService.SendEmail(message);
        //    return StatusCode(200,"ok");
        //}
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return StatusCode(200, "Email Verified Successfully");
                
                }
            }
            return StatusCode(500, "This User Doesnot exist!");
          
        }

    }
}
