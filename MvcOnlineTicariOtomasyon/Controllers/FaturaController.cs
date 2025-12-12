using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.NewFolder1.siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class FaturaController : Controller
    {
        public ActionResult Index()
        {
            using (var c = new Context())
            {
                var liste = c.Faturalars.ToList();
                return View(liste);
            }
        }

        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FaturaEkle(Faturalar f)
        {
            using (var c = new Context())
            {
                c.Faturalars.Add(f);
                c.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult FaturaGetir(int id)
        {
            using (var c = new Context())
            {
                var fatura = c.Faturalars.Find(id);
                if (fatura == null)
                {
                    TempData["Hata"] = "Fatura bulunamadı!";
                    return RedirectToAction("Index");
                }
                return View("FaturaGetir", fatura);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FaturaGuncelle(Faturalar f)
        {
            using (var c = new Context())
            {
                var fatura = c.Faturalars.Find(f.Faturaid);
                if (fatura == null)
                {
                    TempData["Hata"] = "Fatura bulunamadı!";
                    return RedirectToAction("Index");
                }
                fatura.FaturaSeriNo = f.FaturaSeriNo;
                fatura.FaturaSıraNo = f.FaturaSıraNo;
                fatura.Saat = f.Saat;
                fatura.Tarih = f.Tarih;
                fatura.TeslimAlan = f.TeslimAlan;
                fatura.TeslimEden = f.TeslimEden;
                fatura.VergiDairesi = f.VergiDairesi;
                c.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult FaturaDetay(int id)
        {
            using (var c = new Context())
            {
                var degerler = c.FaturaKalems.Where(x => x.Faturaid == id).ToList();
                return View(degerler);
            }
        }

        [HttpGet]
        public ActionResult YeniKalem()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YeniKalem(FaturaKalem p)
        {
            using (var c = new Context())
            {
                c.FaturaKalems.Add(p);
                c.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}