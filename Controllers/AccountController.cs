using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using NETCore.Encrypt.Extensions;
using ProgramlamaYazProje.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using ProgramlamaYazProje.Languages;

namespace ProgramlamaYazProje.Controllers
{
    public class AccountController : Controller
    {

        // [Authorize]
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(IConfiguration configuration, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
               

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Geçersiz giriş denemesi.");
            }

            return View(model);
        }

        private string DoMD5HashedString(string s)
        {
            string md5Salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
            string salted = s + md5Salt;
            string hashed = salted.MD5();
            return hashed;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                
                string hashedPassword = DoMD5HashedString(model.Password);

                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Kullanıcı oluşturuldu, giriş yapalım
                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "member"));
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                
            }

            return View(model);
        }

        public async Task<IActionResult> Profile()
        {
            var user = await ProfileInfoLoader();
            return View();
        }

        private async Task<IdentityUser> ProfileInfoLoader()
        {
            IdentityUser user = await _userManager.GetUserAsync(User);
            return user;
        }

        [HttpPost]
        public async Task<IActionResult> ProfileChangeFullName([Required][StringLength(50)] string? fullname)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.GetUserAsync(User);
                await _userManager.SetUserNameAsync(user, fullname);

                return RedirectToAction(nameof(Profile));
            }
            return View("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> ProfileChangePassword([Required][MinLength(6)][MaxLength(16)] string? password)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.GetUserAsync(User);
            }
            return View("Profile");
        }

        [HttpPost]
        public IActionResult ProfileChangeImage([Required] IFormFile file)
        {
            if (ModelState.IsValid)
            {
               

                return RedirectToAction(nameof(Profile));
            }

            ProfileInfoLoader();
            return View("Profile");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}

