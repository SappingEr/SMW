using SaveMyWord.Models;
using SaveMyWord.Models.Repositories;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;

namespace SaveMyWord.Controllers
{
    public class BaseController : Controller
    {
        protected UserRepository userRepository;

        public BaseController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public SignInManager SignInManager
        {
            get { return HttpContext.GetOwinContext().Get<SignInManager>(); }
        }

        public UserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<UserManager>(); }
        }

        public User CurrentUser
        {
            get { return userRepository.GetCurrentUser(User); }
        }

        public virtual ActionResult RedirectToBackUrl()
        {
            var backUrl = Request["ReturnUrl"];
            var redirectUrl = !string.IsNullOrEmpty(backUrl) ? backUrl : Url.Action("Index");
            return Redirect(redirectUrl);
        }
    }
}