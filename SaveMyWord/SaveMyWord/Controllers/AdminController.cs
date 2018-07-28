using Microsoft.AspNet.Identity;
using SaveMyWord.Models;
using SaveMyWord.Models.Filters;
using SaveMyWord.Models.Repositories;
using System.Web.Mvc;

namespace SaveMyWord.Controllers
{
    [Authorize(Users = "Admin")]
    public class AdminController : BaseController
    {
        public AdminController(UserRepository userRepository) :
            base(userRepository)
        {
            this.userRepository = userRepository;
        }

        public ActionResult Index(UserFilter userFilter, FetchOptions options)
        {
            var users = userRepository.Find(userFilter, options);
            return View(users);
        }

        public ActionResult Delete(long id)
        {
            var user = userRepository.Load(id);
            userRepository.Delete(user);
            return RedirectToBackUrl();
        }


        public ActionResult Edit(long id)
        {
            var user = userRepository.Load(id);
            return View(new UserViewModel { Entity = user });
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel model)
        {
            userRepository.InvokeInTransaction(() => {
                var user = userRepository.Load(model.Entity.Id);
                user.UserName = model.Entity.UserName;
                user.Email = model.Entity.Email;
                userRepository.Save(user);
            });
            return RedirectToBackUrl();
        }

        public ActionResult Create()
        {
            var model = new UserViewModel { Entity = new User() };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var res = UserManager.CreateAsync(model.Entity, model.Password);
                if (res.Result == IdentityResult.Success)
                {
                    return RedirectToBackUrl();
                }
            }
            return RedirectToBackUrl();
        }

        
    }
}