using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Lang.Models;
using Lang.Models.AccountViewModels;
using Lang.Services;
using Lang.Helpers;
using Lang.Data;
using Newtonsoft.Json;

namespace Lang.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _db;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ApplicationDbContext db,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
            _logger = logger;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        [Route("account/login")]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("account/lockout")]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("account/logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        [Route("account/sign-up")]
        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("account/external-login")]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("account/external-login-callback")]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToAction(nameof(Login));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: true, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in with {Name} provider.", info.LoginProvider);
                return Redirect("~/");
            }
            if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                HttpContext.Session.Set("SignUpProcess", new SignUpProcess { Profile = new ProfileViewModel(info) });
                return RedirectToAction(nameof(SignUpProfile));
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("account/sign-up-profile")]
        public IActionResult SignUpProfile()
        {
            var model = HttpContext.Session.Get<SignUpProcess>("SignUpProcess").Profile;
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("account/sign-up-profile")]
        public async Task<IActionResult> SignUpProfile(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    throw new ApplicationException("Error loading external login information during confirmation.");
                }

                var signUpProcess = HttpContext.Session.Get<SignUpProcess>("SignUpProcess");
                signUpProcess.Profile = model;
                HttpContext.Session.Set("SignUpProcess", signUpProcess);

                return RedirectToAction(nameof(SignUpLanguages));
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("account/sign-up-languages")]
        public async Task<IActionResult> SignUpLanguages()
        {
            var userLanguages = HttpContext.Session.Get<SignUpProcess>("SignUpProcess").UserLanguages;
            var model = new LanguagesViewModel();
            model.UserLanguagesJson = JsonConvert.SerializeObject(userLanguages);
            await model.BuildAsync(_db);
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("account/sign-up-languages")]
        public async Task<IActionResult> SignUpLanguages(LanguagesViewModel model)
        {
            var signUpProcess = HttpContext.Session.Get<SignUpProcess>("SignUpProcess");
            signUpProcess.UserLanguages = model.GetUserLanguages();

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }
            var user = new User
            {
                UserName = $"{info.LoginProvider}-{info.ProviderKey}",
                Email = signUpProcess.Profile.Email,
                Name = signUpProcess.Profile.Name,
                Country = signUpProcess.Profile.Country,
                Gender = signUpProcess.Profile.Gender,
                BirthYear = signUpProcess.Profile.GetBirthYear()
            };
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                result = await _userManager.AddLoginAsync(user, info);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                    user.Languages = new List<UserLanguage>();
                    foreach(var language in signUpProcess.UserLanguages)
                    {
                        user.Languages.Add(new UserLanguage
                        {
                            LanguageId = language.Key,
                            Level = language.Value
                        });
                    }
                    await _db.SaveChangesAsync();
                    return Redirect("~/");
                }
            }
            AddErrors(result);
            await model.BuildAsync(_db);
            return View(model);
        }

        [HttpGet]
        [Route("account/access-denied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        #endregion
    }
}
