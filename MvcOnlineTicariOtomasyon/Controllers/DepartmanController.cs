using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.NewFolder1.siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class DepartmanController : Controller
    {
        public ActionResult Index()
        {
            using (var c = new Context())
            {
                var degerler = c.Departmans.Where(x => x.Durum == true).ToList();
                return View(degerler);
            }
        }

        [HttpGet]
        public ActionResult DepartmanEkle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DepartmanEkle(Departman d)
        {
            using (var c = new Context())
            {
                d.Durum = true;
                c.Departmans.Add(d);
                c.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DepartmanSil(int id)
        {
            using (var c = new Context())
            {
                var dep = c.Departmans.Find(id);
                if (dep == null)
                {
                    TempData["Hata"] = "Departman bulunamadı!";
                    return RedirectToAction("Index");
                }
                dep.Durum = false;
                c.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult DepartmanGetir(int id)
        {
            using (var c = new Context())
            {
                var dpt = c.Departmans.Find(id);
                if (dpt == null)
                {
                    TempData["Hata"] = "Departman bulunamadı!";
                    return RedirectToAction("Index");
                }
                return View("DepartmanGetir", dpt);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DepartmanGuncelle(Departman p)
        {
            using (var c = new Context())
            {
                var dept = c.Departmans.Find(p.Departmanid);
                if (dept == null)
                {
                    TempData["Hata"] = "Departman bulunamadı!";
                    return RedirectToAction("Index");
                }
                dept.DepartmanAd = p.DepartmanAd;
                c.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult DepartmanDetay(int id)
        {
            using (var c = new Context())
            {
                var degerler = c.Personels.Where(x => x.Departmanid == id).ToList();
                var dpt = c.Departmans.Where(x => x.Departmanid == id).Select(y => y.DepartmanAd).FirstOrDefault();
                ViewBag.d = dpt;
                return View(degerler);
            }
        }

        public ActionResult DepartmanPersonelSatis(int id)
        {
            using (var c = new Context())
            {
                var degerler = c.SatisHarekets.Include("Urun").Include("Cariler").Where(x => x.Personelid == id).ToList();
                var per = c.Personels.Where(x => x.Personelid == id).Select(y => y.PersonelAd + " " + y.PersonelSoyad).FirstOrDefault();
                ViewBag.dpers = per;
                return View(degerler);
            }
        }
    }
}