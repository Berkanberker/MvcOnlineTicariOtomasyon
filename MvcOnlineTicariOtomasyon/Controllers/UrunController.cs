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
    public class UrunController : Controller
    {
        public ActionResult Index(string p)
        {
            using (var c = new Context())
            {
                var urunler = c.Uruns.Include("Kategori").Where(x => x.Durum == true);
                if (!string.IsNullOrEmpty(p))
                {
                    urunler = urunler.Where(y => y.UrunAd.Contains(p));
                }
                return View(urunler.ToList());
            }
        }

        [HttpGet]
        public ActionResult YeniUrun()
        {
            using (var c = new Context())
            {
                List<SelectListItem> deger1 = (from x in c.Kategoris
                                               select new SelectListItem
                                               {
                                                   Text = x.KategoriAd,
                                                   Value = x.KategoriID.ToString()
                                               }).ToList();
                ViewBag.dgr1 = deger1;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YeniUrun([Bind(Exclude = "Urunid")] Urun p)
        {
            using (var c = new Context())
            {
                p.Durum = true;
                c.Uruns.Add(p);
                c.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UrunSil(int id)
        {
            using (var c = new Context())
            {
                var deger = c.Uruns.Find(id);
                if (deger == null)
                {
                    TempData["Hata"] = "Ürün bulunamadı!";
                    return RedirectToAction("Index");
                }
                deger.Durum = false;
                c.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult UrunGetir(int id)
        {
            using (var c = new Context())
            {
                List<SelectListItem> deger1 = (from x in c.Kategoris
                                               select new SelectListItem
                                               {
                                                   Text = x.KategoriAd,
                                                   Value = x.KategoriID.ToString()
                                               }).ToList();
                ViewBag.dgr1 = deger1;

                var urundeger = c.Uruns.Find(id);
                if (urundeger == null)
                {
                    TempData["Hata"] = "Ürün bulunamadı!";
                    return RedirectToAction("Index");
                }
                return View("UrunGetir", urundeger);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UrunGuncelle(Urun p)
        {
            using (var c = new Context())
            {
                var urn = c.Uruns.Find(p.Urunid);
                if (urn == null)
                {
                    TempData["Hata"] = "Ürün bulunamadı!";
                    return RedirectToAction("Index");
                }
                urn.AlisFiyat = p.AlisFiyat;
                urn.Durum = p.Durum;
                urn.Kategoriid = p.Kategoriid;
                urn.Marka = p.Marka;
                urn.SatisFiyat = p.SatisFiyat;
                urn.Stok = p.Stok;
                urn.UrunAd = p.UrunAd;
                urn.UrunGorsel = p.UrunGorsel;
                c.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult UrunListesi()
        {
            using (var c = new Context())
            {
                var degerler = c.Uruns.Include("Kategori").Where(x => x.Durum == true).ToList();
                return View(degerler);
            }
        }

        [HttpGet]
        public ActionResult SatisYap(int id)
        {
            using (var c = new Context())
            {
                List<SelectListItem> deger3 = (from x in c.Personels
                                               select new SelectListItem
                                               {
                                                   Text = x.PersonelAd + " " + x.PersonelSoyad,
                                                   Value = x.Personelid.ToString()
                                               }).ToList();

                ViewBag.dgr3 = deger3;
                var deger1 = c.Uruns.Find(id);
                if (deger1 == null)
                {
                    TempData["Hata"] = "Ürün bulunamadı!";
                    return RedirectToAction("Index");
                }
                ViewBag.dgr1 = deger1.Urunid;
                ViewBag.dgr2 = deger1.SatisFiyat;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SatisYap(SatisHareket p)
        {
            using (var c = new Context())
            {
                p.Tarih = DateTime.Today;
                c.SatisHarekets.Add(p);
                c.SaveChanges();
                return RedirectToAction("Index", "Satis");
            }
        }
    }
}
