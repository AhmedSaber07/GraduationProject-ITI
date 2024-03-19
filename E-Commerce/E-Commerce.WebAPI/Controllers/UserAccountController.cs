using Company.Dtos.ViewResult;
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

        UserAccountController(SignInManager<MyUser> signInManager,UserManager<MyUser> userManager)
        {
            _SignInManager = signInManager;
            _userManager = userManager;
        }
        //[HttpPost("Login")]
        //public async Task<IActionResult> Login(LoginDto loginDto)
        //{



        //}
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
             
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
            MyUser user = new MyUser() { Email = registerDto.Email, UserName = registerDto.Phone };
            var result= await _userManager.CreateAsync(user,registerDto.Password);
            if (result.Succeeded)
            {
                return Ok("Created Successfuly");
            }
            return StatusCode(500, "Error exist");

        }





    }
}
