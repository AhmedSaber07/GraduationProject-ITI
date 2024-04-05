﻿using Azure;
using Company.Dtos.ViewResult;
using E_Commerce.Application.Services;
using E_Commerce.Application.Settings;
using E_Commerce.Domain.DTOs.UserAccount;
using E_Commerce.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
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

        private readonly UserManager<MyUser> _userManager;
        private readonly SignInManager<MyUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IuserService _userService;
        private readonly ILogger<LoginDto> _logger;


        public UserAccountController(SignInManager<MyUser> signInManager,
            UserManager<MyUser> userManager, IConfiguration configuration,
            RoleManager<IdentityRole<Guid>> roleManager
            , IEmailService emailService
            , IuserService userService
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _emailService = emailService;
            _userService = userService;
        }
        #region ExternalLogin
        //[HttpGet("ExternalLoginCallback")]
        //[AllowAnonymous]
        //public async Task<IActionResult>ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        //{
        //    returnUrl = returnUrl ?? Url.Content("~/");


        //    ExternalLogins =
        //    (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();


        //    if (remoteError != null)
        //    {
        //        ModelState.AddModelError(string.Empty,
        //            $"Error from external provider: {remoteError}");
        //        return Ok(returnUrl);
        //    }

        //    var info = await _signInManager.GetExternalLoginInfoAsync();
        //    if (info == null)
        //    {
        //        ModelState.AddModelError(string.Empty,
        //            "Error loading external login information.");

        //    return Ok(ExternalLogins);
        //}

        //var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        //MyUser user = null;

        //if (email != null)
        //{
        //    user = await _userManager.FindByEmailAsync(email);

        //    if (user != null && !user.EmailConfirmed)
        //    {
        //        ModelState.AddModelError(string.Empty, "Email not confirmed yet");
        //        return Ok(ExternalLogins);
        //    }
        //}

        //var signInResult = await _signInManager.ExternalLoginSignInAsync(
        //                            info.LoginProvider, info.ProviderKey,
        //                            isPersistent: false, bypassTwoFactor: true);

        //if (signInResult.Succeeded)
        //{
        //    return LocalRedirect(returnUrl);
        //}
        //else
        //{
        //if (email != null)
        //{
        //    if (user == null)
        //    {
        //        user = new MyUser
        //        {
        //            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
        //            Email = info.Principal.FindFirstValue(ClaimTypes.Email)
        //        };

        //        await _userManager.CreateAsync(user);

        //        // After a local user account is created, generate and log the
        //        // email confirmation link
        //        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        //        var confirmationLink = Url.Action("ConfirmEmail", "Account",
        //                        new { userId = user.Id, token = token }, Request.Scheme);


        //ViewBag.ErrorTitle = "Registration successful";
        //ViewBag.ErrorMessage = "Before you can Login, please confirm your " +
        //    "email, by clicking on the confirmation link we have emailed you";
        //return View("Error");


        //    await _userManager.AddLoginAsync(user, info);
        //    await _signInManager.SignInAsync(user, isPersistent: false);

        //    return LocalRedirect(returnUrl);
        //}


        //ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
        //ViewBag.ErrorMessage = "Please contact support on Pragim@PragimTech.com";

        //        //return View("Error");
        //    }
        //    return Ok();
        //}
        //[HttpGet("external-logins")]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetExternalLogins(string returnUrl = null)
        //{
        //    returnUrl ??= Url.Content("~/");

        //    var externalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        //    return Ok(new { returnUrl, externalLogins });
        //}

        //[HttpPost("external/sign-in", Name = "PostExternalLogin")]
        //public IActionResult ExternalLogin(string provider, string returnTo = null)
        //{
        //    var redirectUrl = Url.RouteUrl("GetExternalLoginCallBack", new { returnTo }, Request.Scheme);

        //    var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

        //    return Challenge(properties, provider);
        //}
        //[HttpGet("external/call-back", Name = "GetExternalLoginCallBack")]
        //public async Task<IActionResult> ExternalLoginCallBack(string returnTo = null, string remoteError = null)
        //{
        //    if (!string.IsNullOrEmpty(remoteError) || !string.IsNullOrWhiteSpace(remoteError))
        //    {
        //        return StatusCode(500, "please enter the remote provider");
        //    }

        //    var info = await _signInManager.GetExternalLoginInfoAsync();
        //    if (info == null)
        //    {
        //        return StatusCode(500, "no information about this login");
        //    }

        //    var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

        //    if (result.Succeeded)
        //    {
        //        await _signInManager.UpdateExternalAuthenticationTokensAsync(info);

        //        return RedirectToLocal(returnTo);
        //    }
        //   else
        //   {
        //        return StatusCode(500, "errrorrr");
        //   }

        //}
        //[HttpPost("external/confirm", Name = "PostExternalLoginConfirm")]
        //public async Task<IActionResult> ExternalLoginConfirm(RegisterDto model, string returnTo = null)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return StatusCode(500, "errrorrr");
        //    }

        //    var info = await _signInManager.GetExternalLoginInfoAsync();



        //    var user = new MyUser
        //    {
        //        Email = model.Email,
        //        UserName = model.Email,

        //        FirstName = model.FirstName,
        //        LastName = model.LastName,
        //        EmailConfirmed = true
        //    };

        //    var result = await _userManager.CreateAsync(user);

        //    if (result.Succeeded)
        //    {
        //        result = await _userManager.AddLoginAsync(user, info);

        //        if (result.Succeeded)
        //        {
        //            await _signInManager.SignInAsync(user, false);

        //            await _signInManager.UpdateExternalAuthenticationTokensAsync(info);

        //            return RedirectToLocal(returnTo);
        //        }



        //        return Ok();
        //    }
        //    return Ok();
        //}

        //private IActionResult RedirectToLocal(string returnTo)
        //{
        //    return Redirect(Url.IsLocalUrl(returnTo) ? returnTo : "/");
        //}
        #endregion


        private async Task<List<AuthenticationScheme>> ExternalLoginAsync()
      => (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        private AuthenticationProperties AuthenticationProperties(string provider, string returnUrl)
       => (_signInManager.ConfigureExternalAuthenticationProperties(provider, returnUrl));
        private async Task<bool> CreateUserWithExternalLoginCallBackAsync()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return false;
            var result = await _signInManager
                .ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
                return true;
            else
            {
                string email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (email is not null)
                {
                    MyUser user = await _userManager.FindByEmailAsync(email);
                    if (user != null)
                    {
                        await _userManager.AddLoginAsync(user, info);
                        await _signInManager.SignInAsync(user, false);
                        return true;
                    }
                    else
                    {
                        user = new MyUser()
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Name),
                            Email = email,
                        };
                        IdentityResult createuserResult = await _userManager.CreateAsync(user);
                        if (createuserResult.Succeeded)
                        {
                            createuserResult = await _userManager.AddToRoleAsync(user, "User");
                            IdentityResult createuserLogins = await _userManager.AddLoginAsync(user, info);
                            if (createuserLogins.Succeeded)
                            {
                                await _signInManager.SignInAsync(user, false);
                                return true;
                            }
                        }
                        return createuserResult.Succeeded;
                    }
                }
            }
            return false;
        }

        [HttpGet("ExternalLogin")]
        public async Task<IActionResult> ExternalLogin()
        {
            var ex = await ExternalLoginAsync();
            var properties = AuthenticationProperties(ex.FirstOrDefault().Name, "./api/UserAccount/Callback");//redirectUrl);
            return new ChallengeResult(ex.FirstOrDefault().Name, properties);
        }
        [HttpGet("Callback")]
        public async Task<IActionResult> Callback()
        {
            bool result = await CreateUserWithExternalLoginCallBackAsync();
            return Ok(result);
        }


        [Authorize]
        [HttpPost("AddAddress")]
        public async Task<IActionResult> AddAddress(AddressDto addressDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.AddAddress(addressDto);
            if (result)
                return StatusCode(200, "Updated successfuly");
            else
                return StatusCode(401, "Unauthorized");

        }
       // [Authorize]
        [HttpDelete("DeleteAddress")]
        public async  Task<IActionResult> DeleteAddress(string  Email)
        {
          
               bool x=await _userService.DeleteAddress(Email);
              if(x)
                return StatusCode(200, " Deleted successfuly");
              else
                return StatusCode(500, "Not  Deleted ");


        }
        [Authorize(Roles = "Admin, User")]
        [HttpPut("{oldEmail}/email")]
        public async Task<IActionResult> UpdateEmail(string oldEmail, [FromBody] string newEmail)
        {
            var user = await _userManager.FindByEmailAsync(oldEmail);
            if (user == null)
            {
                return NotFound();
            }

            user.Email = newEmail;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
        /// <summary>
        ///     [Authorize(Roles = "Admin, User")]
        /// </summary>
        /// <param name="oldPhone"></param>
        /// <param name="newPhone"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, User")]
        [HttpPost("UpdatePhone")]
        public async Task<IActionResult> UpdatePhone(string oldPhone, string newPhone)
        {
            var user = await _userManager.FindByNameAsync(oldPhone);
            if (user == null)
            {
                return NotFound();
            }

            user.UserName = newPhone;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("The phone updated");
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return StatusCode(200, "Logout successfuly");



        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto, string role)
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
            exist = await _userManager.FindByNameAsync(registerDto.PhoneNumber);

            if (exist != null)
            {

                return StatusCode(501, "The PhoneNumber already exist");
            }
            /////
            MyUser user = new MyUser() { FirstName = registerDto.FirstName, LastName = registerDto.LastName, Email = registerDto.Email, UserName = registerDto.PhoneNumber, SecurityStamp = Guid.NewGuid().ToString() };
            var result1 = await _roleManager.RoleExistsAsync(role);

            if (result1)
            {
                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (!result.Succeeded)
                {
                    return StatusCode(500, result.Errors);
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, role);
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action(nameof(ConfirmEmail), "UserAccount", new { token, email = user.Email }, Request.Scheme);
                    var message = new Message(new string[] { user.Email }, "Confirmation email link", confirmationLink);
                    _emailService.SendEmail(message);
                    return StatusCode(200, "Created Successfuly");
                }
            }
            else
            {
                return StatusCode(500, "You Entered Role Not found");
            }

        }

        [Authorize]
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword(string Email, string NewPassword, string oldPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, oldPassword, NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                return BadRequest(changePasswordResult.Errors);
            }

            return Ok("Password changed successfully");
        }

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
            if (isValidEmail)
                user = await _userManager.FindByEmailAsync(logindto.UserName);
            else
                user = await _userManager.FindByNameAsync(logindto.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, logindto.Password))
            {
                if (!user.EmailConfirmed)
                {
                    return Unauthorized("Email is not confirmed.");
                }
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                     new Claim(ClaimTypes.Name, user.FirstName),
                      new Claim(ClaimTypes.Name, user.LastName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                RegisterDto sendDto = new RegisterDto() { FirstName = user.FirstName, LastName = user.LastName, Email = user.Email, PhoneNumber = user.UserName, Password = null };

                var jwtToken = GetToken(authClaims);

                return StatusCode(200, new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo,
                    _user = sendDto

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
        #region Oldreset
        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            var model = new ResetPasswordDto { token = token, Email = email };

            return Ok(new { model });
        }
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
        [HttpPost("forget-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var link = Url.Action(nameof(ResetPassword), "UserAccount", new { token, email = user.Email }, Request.Scheme);
                var message = new Message(new string[] { user.Email }, "reset password link", link!);
                _emailService.SendEmail(message);
                return StatusCode(200, "Password change request is sent on email");
            }
            return StatusCode(500, "Error exist");
        }
        #endregion

        [HttpPost("NewResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> NewResetPassword([FromBody] Domain.DTOs.UserAccount.ResetPasswordRequest request)
        {

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Password reset successfully");

        }

        [HttpGet("{userId}/email")]
        public async Task<IActionResult> GetUserEmailById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var email = user.Email;

            return Ok(new { Email = email });
        }
        [HttpGet("userid")]
        public async Task<IActionResult> GetUserIdByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.Id);
        }

        [HttpPost("SendCode")]
        public async Task<IActionResult> SendCode(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            int code = Generate4DigitNumber();
            user.ResetCode = code;
            await _userManager.UpdateAsync(user);
            string m = "A request was made to change your password recently." +
                " If you are the one who made this request, this is the code that you can use\n" +
                $"<br><b>{code}</b><br>"
                + "\nIf you do not make this request, you can ignore this email and your password will remain as it is.";
            var message = new Message(new string[] { user.Email }, "reset password code", m);
            _emailService.SendEmail(message);
            return StatusCode(200, "Password change request is sent on email");
        }
        private static readonly Random _random = new Random();
        private static int Generate4DigitNumber()
        {
            return _random.Next(1000, 10000);
        }
        [HttpPost("CheckCode")]
        public async Task<bool> CheckCode(int code, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }
            return (user.ResetCode == code);
        }
        [HttpGet("GetUsersData")]
        public async Task<IActionResult> GetUsersData()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersData = users.Select(u => new GetUsersData
            {
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                phone = u.UserName,
                addressLine1 = u.addressLine1,
                addressLine2 = u.addressLine2,
                city = u.city,
                country = u.country
            }).ToList();

            return Ok(usersData);
        }
       // [Authorize]
        [HttpGet("GetUserAddress")]
        public async Task<IActionResult> GetUserAddress(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var userData = new
            {             
                Phone = user.UserName,
                AddressLine1 = user.addressLine1,
                AddressLine2 = user.addressLine2,
                City = user.city,
                Country = user.country
            };

            return Ok(userData);
        }
    
    }
}
