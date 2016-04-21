using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using securityMandatory2.Models;

namespace securityMandatory2.Controllers
{
    public class HomeController : Controller
    {
        private readonly Repository _repository = new Repository();

        public ActionResult Index()
        {
            return View(GetUserFromCookie());
        }

        #region [Create User]

        [HttpGet]
        public ActionResult CreateUser()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public ActionResult CreateUser(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("Error", "Password matcher ikke");
                return View(model);
            }
            if (_repository.CheckUserExist(model))
            {
                ModelState.AddModelError("Error", "Bruger eksisterer. Indtast andet brugernavn");
                return View(model);
            }

            _repository.CreateUser(model);
            TempData["message"] = "Bruger oprettet";
            return Redirect("/Home");
        }

        #endregion [Create User]

        #region [Login]

        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var user = _repository.UserLoginCheck(model.Username, model.Password);
            if (user != null)
            {
                SetCookieAndLoginUser(user);
                TempData["message"] = "Login lykkedes";
                return Redirect("/Home");
            }
            else
            {
                ModelState.AddModelError("Error", "Forkert brugernavn eller password");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            TempData["message"] = "Logout lykkedes";
            return Redirect("/Home");
        }

        #endregion [Login]

        #region [Session Cookie]

        private void SetCookieAndLoginUser(User user)
        {
            var serializer = new JavaScriptSerializer();
            var userData = serializer.Serialize(user);
            var authTicket = new FormsAuthenticationTicket(
                1,
                user.Username,
                DateTime.Now,
                DateTime.Now.AddMonths(1),
                user.RememberMe, // persistant
                userData);
            var encTicket = FormsAuthentication.Encrypt(authTicket);
            var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            if (user.RememberMe) faCookie.Expires = DateTime.Now.AddYears(10);
            Response.Cookies.Add(faCookie);
        }

        private User GetUserFromCookie()
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
                return null;
            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
            if (ticket == null) return null;
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<User>(ticket.UserData);
        }

        #endregion [Session Cookie]

    }
}