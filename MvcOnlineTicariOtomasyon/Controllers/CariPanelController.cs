using MvcOnlineTicariOtomasyon.Models.NewFolder1.siniflar;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class CariPanelController : Controller
    {
        public ActionResult Index()
        {
            using (var c = new Context())
            {
                var mail = (string)Session["CariMail"];
                ViewBag.m = mail;

                var mailid = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.Cariid).FirstOrDefault();
                ViewBag.mid = mailid;

                var toplamsatis = c.SatisHarekets.Where(x => x.Cariid == mailid).Count();
                ViewBag.toplamsatis = toplamsatis;

                // Nullable decimal kullanarak null hatasını önlüyoruz
                var toplamtutar = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => (decimal?)y.ToplamTutar) ?? 0;
                ViewBag.toplamtutar = toplamtutar;

                var toplamurunsayisi = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => (int?)y.Adet) ?? 0;
                ViewBag.toplamurunsayisi = toplamurunsayisi;

                var adsoyad = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CarSoyad).FirstOrDefault();
                ViewBag.adsoyad = adsoyad;

                return View();
            }
        }

        public ActionResult Siparislerim()
        {
            using (var c = new Context())
            {
                var mail = (string)Session["CariMail"];
                var id = c.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.Cariid).FirstOrDefault();
                var degerler = c.SatisHarekets.Include("Urun").Where(x => x.Cariid == id).ToList();
                return View(degerler);
            }
        }
    }
}