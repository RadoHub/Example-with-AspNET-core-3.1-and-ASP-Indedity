using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Prj.Bus.Abstract;
using Prj.WebUI.EmailServices;
using Prj.WebUI.Extensions;
using Prj.WebUI.Identity;
using Prj.WebUI.Models;
using SendGrid.Helpers.Mail;

namespace Prj.WebUI.Controllers
{
    [AutoValidateAntiforgeryToken] 
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager; 
        private SignInManager<ApplicationUser> _signInManager;
        private IEmailSender _emailSender;
        private ICartService _cartService;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender, ICartService cartService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailSender = emailSender;
            _cartService = cartService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {

            var user = new ApplicationUser() {
             UserName = model.UserName,
             FullName = model.FullName,
             Email = model.Email
            
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var tokencode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callBackUrl = Url.Action("ConfirmEmail", "Account", new 
                { 
                    userId= user.Id , 
                    token = tokencode 
                });

                var mailbody = "Hi mr <b>" + model.UserName+ $"<b> Please <a href='http://localhost:54487{callBackUrl}'>click here</a> to activate your accout";
                await _emailSender.SendEmailAsync(model.Email, "Account Confirmation Mail", mailbody);

                ModelState.AddModelError("", "A confirmation mail has sent to you mail adress");
                return RedirectToAction("login", "account");
            }
            ModelState.AddModelError("", "An unknown error has occured");
            return View(model);
        }

        [HttpGet]
        public IActionResult Login (string ReturnUrl=null)
        {
            if (User.Identity.IsAuthenticated)
            {
                TempData.Put("message", new ResultMessage { Css = "info", Title="Information", Message="You are already logged in"}); 
                return Redirect("~/");
            }
            return View(new LoginModel() { 
             ReturnUrl = ReturnUrl  
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model,  bool isChecked)
        {
           
            if (!ModelState.IsValid)
            {
                TempData.Put("message", new ResultMessage() { 
                 Css="danger",
                 Title="Attention!",
                 Message="An unknown error occurred! Please retry the process"
                });
                return View(model);
            }

         

            var userWithEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userWithEmail == null)
            {
                TempData.Put("message", new ResultMessage {
                 Css="warning",
                 Title="Warning!",
                 Message="There is noy any user With This Email!"
                });
                return View(model);
            }
            if (!await _userManager.IsEmailConfirmedAsync(userWithEmail))
            {
                TempData.Put("message", new ResultMessage() {
                Css="info",
                Title="Information !",
                Message="Pease Confirm Your Email Account"
                });
                return View(model);
            }

           

            var result = await _signInManager.PasswordSignInAsync(userWithEmail, model.Password, isChecked, false);
            if (result.Succeeded)
            {
                TempData.Put("message", new ResultMessage() { Css="info", Title="Information!", Message="You have just logged in" });
                return Redirect(model.ReturnUrl ?? "~/"); 
            }
            TempData.Put("message", new ResultMessage() { Css="warning", Title="Warning!", Message="System could not find any user with given details." });            
            return View(model) ;
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }

        public async Task<IActionResult> ConfirmEmail (string userId , string token)
        {
            if (userId==null || token == null)
            {
                TempData.Put("message", new ResultMessage()
                {
                    Css = "warning",
                    Title = "Warning!",
                    Message = "Invalid validation Proccess"
                });
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                   
                    _cartService.InitializeCart(user.Id);  

                    TempData.Put("message", new ResultMessage() {
                    Css= "success",
                    Title="Information!",
                    Message="Email has just confirmed"
                    });
                    return RedirectToAction("Login", "account");
                }
            }

            TempData.Put("message", new ResultMessage() { 
            Css="warning",
            Title= "Warning",
            Message = "Confirmation phase is not succeeded"
            });
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                TempData.Put("message", new ResultMessage() { Css = "warning", Title = "Attention !", Message = "Please enter an Email!" });
                return View();
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                TempData.Put("message", new ResultMessage() { Css = "warning", Title = "Warning", Message = "System could not find any user with given details" });
                return View();
            }
            var tokenCode = await _userManager.GeneratePasswordResetTokenAsync(user);


            var callBackUrl = Url.Action("resetpassword", "account", new {  token = tokenCode });
            var mailbody = "Hello dear" + user.UserName + $"please click <a href='http://localhost:54487{callBackUrl}'> here </a> to get resetpassword email";
            await _emailSender.SendEmailAsync(email, "Reset Password Mail", mailbody);            
            return RedirectToAction("Login", "Account");

        }

        [HttpGet]
        public IActionResult ResetPassword( string token)
        {
   
            if (token==null)
            {
                TempData.Put("message", new ResultMessage() { Css="warning", Title="Warning !", Message="Process is not valid, maka a new password request please." });
                return RedirectToAction("index", "home" );
            }

            var model = new ResetPasswordModel() { Token = token };
            return View(model);
        }

        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData.Put("message", new ResultMessage() { Css = "danger", Title = "Error", Message = "An unknown error occured, please try again." });
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                TempData.Put("message", new ResultMessage() { Css = "danger", Title = "Error", Message = "Data is not valid." });
                return RedirectToAction("index", "home");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                TempData.Put("message", new ResultMessage() { Css = "info", Title = "Information", Message = "Your Password has changed, please log in." });
                return RedirectToAction("login", "account");
            }
            TempData.Put("message", new ResultMessage() { Css = "danger", Title = "Error", Message = "Data is not valid." });
            return View(model);
        }

        public IActionResult Accessdenied()
        {
            return View();
        }
    }
}