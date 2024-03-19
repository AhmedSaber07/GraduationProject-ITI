using Azure;
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
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public UserAccountController(SignInManager<MyUser> signInManager, UserManager<MyUser> userManager, IConfiguration configuration, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _SignInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
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
                await _userManager.AddToRoleAsync(user, "user");
                if (!result.Succeeded)
                {
                    StatusCode(500, "Error exist");
                }
                else 
                {
                    return Ok("Created Successfuly");
                }
            }         
            return StatusCode(500, "Error exist");

        }





    }
}
