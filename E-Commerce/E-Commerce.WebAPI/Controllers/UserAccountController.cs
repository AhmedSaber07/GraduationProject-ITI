using Azure;
using Company.Dtos.ViewResult;
using E_Commerce.Application.Services;
using E_Commerce.Application.Settings;
using E_Commerce.Domain.DTOs.UserAccount;
using E_Commerce.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

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
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto logindto)
        {
            var isValidEmail = new EmailAddressAttribute().IsValid(logindto.UserName);
            MyUser user;
            if(isValidEmail)
                user = await _userManager.FindByEmailAsync(logindto.UserName);     
            else
                user = await _userManager.FindByNameAsync(logindto.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, logindto.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }


                var jwtToken = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo
                });
               
            }
            return Unauthorized();


        }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword(string email,string token)
        {
            var model = new ResetPasswordDto { token = token, Email = email };

            return Ok(new { model });
        }
        //ResetPasswordDto
        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);

            if (user != null)
            {
                var resetPasswordRes = await _userManager.ResetPasswordAsync(user, resetPasswordDto.token, resetPasswordDto.Password);
                if (resetPasswordRes.Succeeded)
                {
                    foreach (var item in resetPasswordRes.Errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }
                    return Ok(ModelState);
                }
                return StatusCode(200, "Password has been changed");
            }
            return StatusCode(500, "Error exist");
        }
        //ResetPasswordDto
        [HttpPost("forget-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user!=null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var link=Url.Action(nameof(ResetPassword), "UserAccount",new {token,email=user.Email},Request.Scheme);
                var message = new Message(new string[] { user.Email }, "reset password link", link!);
                _emailService.SendEmail(message);
                return StatusCode(200, "Password change request is sent on email");
            }
            return StatusCode(500, "Error exist");
        }
    }
}
