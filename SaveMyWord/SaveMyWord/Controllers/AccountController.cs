using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaveMyWord.Models;
using SaveMyWord.Models.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;


namespace SaveMyWord.Controllers
{
    
    public class AccountController : BaseController
    {
        public AccountController(UserRepository userRepository)
            : base(userRepository)
        {
        }

        public ActionResult Create()
        {
            var model = new UserViewModel { Entity = new User() };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var res = UserManager.CreateAsync(model.Entity, model.Password);
                if (res.Result == IdentityResult.Success)
                {
                    return RedirectToAction("Login");
                }                
            }
            return RedirectToAction("Login");
        }

        
        public ActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = SignInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (result.Result == SignInStatus.Success)
                {
                    return RedirectToAction("Start", "Note");
                }
                else
                {
                    ModelState.AddModelError("", "Неверный логин или пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            SignInManager.SignOut();
            return RedirectToAction("Start", "Note");
        }

        public ActionResult Info(long id)
        {
            var user = UserManager.FindById(id);
            return View(new UserViewModel { Entity = user });
        }
    }
}