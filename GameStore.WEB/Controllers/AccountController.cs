using GameStore.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GameStore.WEB.Models;
using GameStore.BLL.DTO;
using System.Threading.Tasks;

namespace GameStore.WEB.Controllers
{
    public class AccountController : Controller
    {
        IServices services;

        public AccountController(IServices serv)
        {
            services = serv;
        }

        protected override void Dispose(bool disposing)
        {
            services.Dispose();
            base.Dispose(disposing);
        }

        public AccountController() { }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (services.AccountService.CheckUserLogin(model.Name, model.Password))
                {
                    UserDTO user = services.UserEditor.GetUsers().FirstOrDefault(u=> u.Login == model.Name);
                    if (user.RoleId == 3)
                    {                        
                        FormsAuthentication.SetAuthCookie(model.Name+"Moderator", true);
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("model", "Неверное имя или пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (services.AccountService.CheckDBLogin(model.Name))
                {
                    if (services.AccountService.CheckDBEmail(model.Email))
                    {
                        UserDTO user = new UserDTO { Email = model.Email, Login = model.Name, Password = model.Password };
                        services.AccountService.CreateUser(user);
                        if (services.AccountService.CheckUserLogin(user.Login, user.Password))
                        {
                            FormsAuthentication.SetAuthCookie(model.Name, true);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                        ModelState.AddModelError("", "Пользователь с таким электроным адресом уже существует");
                }
                else
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
            }
            return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


    }
}