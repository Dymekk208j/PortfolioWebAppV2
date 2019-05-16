using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Models.ViewModels;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PortfolioWebAppV2.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
#if DEBUG
        [AllowAnonymous]
        public ActionResult TestLogin()
        {
            LoginViewModel model = new LoginViewModel() { Password = "Damian13", RememberMe = false, UserName = "Dymek" };

            ApplicationUser user = UserManager.Find(model.UserName, model.Password);
            if (user != null)
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                ClaimsIdentity identity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);
            }

            return RedirectToAction("Index", "Home");
        }
#endif
        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        private UserManager<ApplicationUser> UserManager { get; set; }

        public ActionResult DeleteUser(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationUser usr = (from u in db.Users
                                   where u.Id == userId
                                   select u).FirstOrDefault();
            if (usr == null) return RedirectToAction("UserMgt", "AdminPanel");

            db.Entry(usr).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("UserMgt", "AdminPanel");

        }

        public ActionResult UpdateUser(UpdateViewModel updateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("UserMgt", "AdminPanel", new { page = 0 });
            }

            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationUser user = Mapper.Map<UpdateViewModel, ApplicationUser>(updateViewModel);

            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();


            return RedirectToAction("UserMgt", "AdminPanel", new { page = 0 });
        }

        [AllowAnonymous]
        public static string GetAuthorName(string authorId)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            ApplicationUser author = (from x in db.Users
                                      where x.Id == authorId
                                      select x).FirstOrDefault();

            if (author != null) return author.UserName;
            return "";
        }
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", @"Błędny login bądź hasło.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        //[RequireHttps]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //[RequireHttps]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Blocked = false,
                    Email = model.Email
                };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            ClaimsIdentity identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

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
        #endregion
    }
}