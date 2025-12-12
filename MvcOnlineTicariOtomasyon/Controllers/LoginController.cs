using MvcOnlineTicariOtomasyon.Models.NewFolder1.siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult Partial1()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult Partial1(Cariler p)
        {
            using (var c = new Context())
            {
                p.Durum = true;
                c.Carilers.Add(p);
                c.SaveChanges();
                return PartialView();
            }
        }

        [HttpGet]
        public ActionResult CariLogin1()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CariLogin1(Cariler p)
        {
            using (var c = new Context())
            {
                var bilgiler = c.Carilers.FirstOrDefault(x => x.CariMail == p.CariMail && x.CariSifre == p.CariSifre);
                if (bilgiler != null)
                {
                    FormsAuthentication.SetAuthCookie(bilgiler.CariMail, false);
                    Session["CariMail"] = bilgiler.CariMail.ToString();
                    return RedirectToAction("Index", "CariPanel");
                }
                else
                {
                    TempData["Hata"] = "Geçersiz e-posta veya şifre!";
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(Admin p)
        {
            using (var c = new Context())
            {
                var bilgiler = c.Admins.FirstOrDefault(x => x.KullaniciAd == p.KullaniciAd && x.Sifre == p.Sifre);
                if (bilgiler != null)
                {
                    FormsAuthentication.SetAuthCookie(bilgiler.KullaniciAd, false);
                    Session["KullaniciAd"] = bilgiler.KullaniciAd.ToString();
                    return RedirectToAction("Index", "Kategori");
                }
                else
                {
                    TempData["Hata"] = "Geçersiz kullanıcı adı veya şifre!";
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}