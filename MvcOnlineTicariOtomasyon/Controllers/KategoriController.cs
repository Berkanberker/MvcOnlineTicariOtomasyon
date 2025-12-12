using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.NewFolder1.siniflar;
using PagedList;
using PagedList.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class KategoriController : Controller
    {
        public ActionResult Index(int sayfa = 1)
        {
            using (var c = new Context())
            {
                var degerler = c.Kategoris.ToList().ToPagedList(sayfa, 4);
                return View(degerler);
            }
        }

        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KategoriEkle(Kategori k)
        {
            using (var c = new Context())
            {
                c.Kategoris.Add(k);
                c.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KategoriSil(int id)
        {
            using (var c = new Context())
            {
                var ktg = c.Kategoris.Find(id);
                if (ktg == null)
                {
                    TempData["Hata"] = "Kategori bulunamadı!";
                    return RedirectToAction("Index");
                }
                c.Kategoris.Remove(ktg);
                c.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult KategoriGetir(int id)
        {
            using (var c = new Context())
            {
                var kategori = c.Kategoris.Find(id);
                if (kategori == null)
                {
                    TempData["Hata"] = "Kategori bulunamadı!";
                    return RedirectToAction("Index");
                }
                return View("KategoriGetir", kategori);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KategoriGuncelle(Kategori k)
        {
            using (var c = new Context())
            {
                var ktgr = c.Kategoris.Find(k.KategoriID);
                if (ktgr == null)
                {
                    TempData["Hata"] = "Kategori bulunamadı!";
                    return RedirectToAction("Index");
                }
                ktgr.KategoriAd = k.KategoriAd;
                c.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}