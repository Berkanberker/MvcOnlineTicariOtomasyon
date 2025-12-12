using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.NewFolder1.siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class CariController : Controller
    {
        public ActionResult Index()
        {
            using (var c = new Context())
            {
                var degerler = c.Carilers.Where(x => x.Durum == true).ToList();
                return View(degerler);
            }
        }

        [HttpGet]
        public ActionResult YeniCari()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YeniCari([Bind(Exclude = "Cariid,Durum")] Cariler p)
        {
            if (!ModelState.IsValid)
            {
                return View(p);
            }
            using (var c = new Context())
            {
                p.Durum = true;
                c.Carilers.Add(p);
                c.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CariSil(int id)
        {
            using (var c = new Context())
            {
                var cr = c.Carilers.Find(id);
                if (cr == null)
                {
                    TempData["Hata"] = "Cari bulunamadı!";
                    return RedirectToAction("Index");
                }
                cr.Durum = false;
                c.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult CariGetir(int id)
        {
            using (var c = new Context())
            {
                var cari = c.Carilers.Find(id);
                if (cari == null)
                {
                    TempData["Hata"] = "Cari bulunamadı!";
                    return RedirectToAction("Index");
                }
                return View("CariGetir", cari);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CariGuncelle([Bind(Exclude = "Durum,CariSifre")] Cariler p)
        {
            if (!ModelState.IsValid)
            {
                return View("CariGetir", p);
            }
            using (var c = new Context())
            {
                var cari = c.Carilers.Find(p.Cariid);
                if (cari == null)
                {
                    TempData["Hata"] = "Cari bulunamadı!";
                    return RedirectToAction("Index");
                }
                cari.CariAd = p.CariAd;
                cari.CarSoyad = p.CarSoyad;
                cari.CariSehri = p.CariSehri;
                cari.CariMail = p.CariMail;
                c.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult MusteriSatis(int id)
        {
            using (var c = new Context())
            {
                var degerler = c.SatisHarekets.Include("Urun").Include("Personel").Where(x => x.Cariid == id).ToList();
                var cr = c.Carilers.Where(x => x.Cariid == id).Select(y => y.CariAd + " " + y.CarSoyad).FirstOrDefault();
                ViewBag.cari = cr;
                return View(degerler);
            }
        }
    }
}