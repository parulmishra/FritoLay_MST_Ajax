using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using FritoLay.Models;
using FritoLay.Models.Repositories;
using FritoLay.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FritoLay.Controllers
{
    public class UserController : Controller
    {
        private IProductRepository productRepo { get; }
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IProductRepository thisRepo = null)
        {
            _userManager = userManager;
            _signInManager = signInManager;

            if (thisRepo == null)
            {
                this.productRepo = EFProductRepository.Instance;
            }
            else
            {
                this.productRepo = thisRepo;
            }
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            var user = new User { UserName = model.Email };
            IdentityResult createResult = await _userManager.CreateAsync(user, model.Password);
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager
               .PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager
                .PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
            // In above line we are using SignInManagers Asysnchronous PasswordInaAsync method to sign a user in with their credentials.
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
