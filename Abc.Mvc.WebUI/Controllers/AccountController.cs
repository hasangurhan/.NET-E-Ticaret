using Abc.Mvc.WebUI.Entitiy;
using Abc.Mvc.WebUI.Identity;
using Abc.Mvc.WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abc.Mvc.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private DataContext db = new DataContext();
        private UserManager<ApplicationUser> UserManager;
        private RoleManager<ApplicationRole> RoleManager;
        public AccountController()
        {
            var userStore =
                 new UserStore<ApplicationUser>(new IdentityDataContext());

            UserManager = new UserManager<ApplicationUser>(userStore);


            var roleStore =
                new RoleStore<ApplicationRole>(new IdentityDataContext());

            RoleManager = new RoleManager<ApplicationRole>(roleStore);
        }
        [Authorize]
        public ActionResult Index()
        {
            var orders = db.Orders.Where(i => i.Username == User.Identity.Name)
                .Select(i => new UserOrderModel()
                {
                    Id=i.Id,
                    OrderNumber = i.OrderNumber,
                    OrderDate = i.OrderDate,
                    OrderState=i.OrderState,
                    Total=i.Total
                }
                ).OrderByDescending(i=>i.OrderDate).ToList();
            return View(orders);
        }
        // GET: Account

        [Authorize]
        public ActionResult Details(int id)
        {
            var entity = db.Orders.Where(i => i.Id == id)
                .Select(i => new OrderDetailsModel()
                {
                    OrderId = i.Id,
                    OrderNumber = i.OrderNumber,
                    Total = i.Total,
                    OrderDate = i.OrderDate,
                    OrderState = i.OrderState,
                    AdresBasligi = i.AdresBasligi,
                    Adres = i.Adres,
                    Sehir = i.Sehir,
                    Semt = i.Semt,
                    Mahalle = i.Mahalle,
                    PostaKodu = i.PostaKodu,
                    Orderlines = i.OrderLines.Select(a => new OrderLineModel()
                    {
                        ProductId = a.ProductId,
                        ProductName = a.Product.Name.Length > 50 ? a.Product.Name.Substring(0, 47) + "..." : a.Product.Name,
                        Image = a.Product.Image,
                        Quantity = a.Quantity,
                        Price = a.Price
                    }).ToList()
                }).FirstOrDefault();

            return View(entity);
        }

      
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                // kayıt islemleri
                var user = new ApplicationUser();
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email;
                user.UserName = model.Username;
               IdentityResult result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    //kullanıcı olustu ve kullanıcıyı role ata
                    if (RoleManager.RoleExists("user"))
                    {
                        UserManager.AddToRole(user.Id, "user");
                    }
                    return RedirectToAction("Login", "Account");
                    
                }
                else
                {
                    ModelState.AddModelError("RegisterUserError","Kullanıcı olusturma hatası");
                }
            }
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model,string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                // login islemleri
                var user = UserManager.Find(model.Username, model.Password);
                if (user!=null)
                {
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var identityclaims = UserManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties();
                    authProperties.IsPersistent = model.RememberMe;
                    authManager.SignIn(authProperties, identityclaims);

                  

                    if (!String.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("LoginUserError", "Böyle bir kullanıcı yok");
                }


            }
            return View(model);
        }
        public ActionResult Logout()
        {

            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}