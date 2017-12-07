using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TradeCenter.Models;

namespace TradeCenter.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private TradeCenterDB _context = new TradeCenterDB();

        //
        // GET: Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Login model)
        {
            var user = _context.Users
                .Where(u => u.UserName == model.UserName
                    && u.Password == model.Password)
                .FirstOrDefault();

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.UserName, true);
                Session["userName"] = user.ToString();
            }
            else
            {
                TempData["error"] = "Incorrect User/Password";
            }

            return RedirectToLocal(Request.UrlReferrer.ToString());
        }

        //
        // GET: /Account/Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["userName"] = null;
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (user == null || !ModelState.IsValid)
            {
                return View();
            }

            using (var ctx = new TradeCenterDB())
            {
                ctx.Users.Add(user);
                ctx.SaveChanges();
            }

            FormsAuthentication.SetAuthCookie(user.UserName, false);
            Session["userName"] = user.ToString();

            return RedirectToLocal(Request.UrlReferrer.ToString());
        }

        //
        // GET: /Account/Edit
        public ActionResult Edit()
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            
            if (user != null)
                return View(user);
            else
                return HttpNotFound();
        }

        //
        // POST: /Account/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (user == null || !ModelState.IsValid)
            {
                return View();
            }

            using (var ctx = new TradeCenterDB())
            {
                User current = ctx.Users.SingleOrDefault(u => u.ID == user.ID);
                current.FirstName = user.FirstName;
                current.LastName = user.LastName;
                current.BirthDate = user.BirthDate;
                current.Email = user.Email;
                current.UserName = user.UserName;
                current.Password = user.Password;
                current.ConfirmPassword = user.ConfirmPassword;
                ctx.SaveChanges();
            }

            FormsAuthentication.SetAuthCookie(user.UserName, false);
            Session["userName"] = user.ToString();

            return RedirectToLocal(Request.UrlReferrer.ToString());
        }

        // Redirects to input url if it's local, otherwise - redirects to Index/Home
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
            base.Dispose(disposing);
        }

        ~AccountController()  // Overrides Object.Finalize()
        {
            Dispose();
        }
    }
}