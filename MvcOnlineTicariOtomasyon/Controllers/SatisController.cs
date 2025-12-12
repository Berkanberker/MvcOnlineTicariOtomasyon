using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MvcOnlineTicariOtomasyon.Models.NewFolder1.siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class SatisController : Controller
    {
        public ActionResult Index()
        {
            using (var c = new Context())
            {
                var degerler = c.SatisHarekets.Include("Urun").Include("Cariler").Include("Personel").ToList();
                return View(degerler);
            }
        }

        [HttpGet]
        public ActionResult YeniSatis()
        {
            using (var c = new Context())
            {
                List<SelectListItem> deger1 = (from x in c.Uruns.Where(u => u.Durum == true)
                                               select new SelectListItem
                                               {
                                                   Text = x.UrunAd,
                                                   Value = x.Urunid.ToString()
                                               }).ToList();

                List<SelectListItem> deger2 = (from x in c.Carilers.Where(ca => ca.Durum == true)
                                               select new SelectListItem
                                               {
                                                   Text = x.CariAd + " " + x.CarSoyad,
                                                   Value = x.Cariid.ToString()
                                               }).ToList();

                List<SelectListItem> deger3 = (from x in c.Personels
                                               select new SelectListItem
                                               {
                                                   Text = x.PersonelAd + " " + x.PersonelSoyad,
                                                   Value = x.Personelid.ToString()
                                               }).ToList();

                ViewBag.dgr1 = deger1;
                ViewBag.dgr2 = deger2;
                ViewBag.dgr3 = deger3;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YeniSatis(SatisHareket s)
        {
            using (var c = new Context())
            {
                using (var transaction = c.Database.BeginTransaction())
                {
                    try
                    {
                        var urun = c.Uruns.Find(s.Urunid);
                        if (urun == null)
                        {
                            TempData["Hata"] = "Ürün bulunamadı!";
                            return RedirectToAction("YeniSatis");
                        }

                        if (urun.Stok >= s.Adet)
                        {
                            urun.Stok = (short)(urun.Stok - s.Adet);
                            s.ToplamTutar = urun.SatisFiyat * s.Adet;
                            s.Tarih = DateTime.Today;
                            c.SatisHarekets.Add(s);
                            c.SaveChanges();
                            transaction.Commit();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["Hata"] = "Yetersiz stok! Mevcut stok: " + urun.Stok;
                            return RedirectToAction("UrunGetir", "Urun", new { id = s.Urunid });
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        TempData["Hata"] = "Satış işlemi başarısız: " + ex.Message;
                        return RedirectToAction("YeniSatis");
                    }
                }
            }
        }

        [HttpGet]
        public ActionResult SatisGetir(int id)
        {
            using (var c = new Context())
            {
                List<SelectListItem> deger1 = (from x in c.Uruns.Where(u => u.Durum == true)
                                               select new SelectListItem
                                               {
                                                   Text = x.UrunAd,
                                                   Value = x.Urunid.ToString()
                                               }).ToList();

                List<SelectListItem> deger2 = (from x in c.Carilers.Where(ca => ca.Durum == true)
                                               select new SelectListItem
                                               {
                                                   Text = x.CariAd + " " + x.CarSoyad,
                                                   Value = x.Cariid.ToString()
                                               }).ToList();

                List<SelectListItem> deger3 = (from x in c.Personels
                                               select new SelectListItem
                                               {
                                                   Text = x.PersonelAd + " " + x.PersonelSoyad,
                                                   Value = x.Personelid.ToString()
                                               }).ToList();

                ViewBag.dgr1 = deger1;
                ViewBag.dgr2 = deger2;
                ViewBag.dgr3 = deger3;

                var deger = c.SatisHarekets.Find(id);
                if (deger == null)
                {
                    TempData["Hata"] = "Satış kaydı bulunamadı!";
                    return RedirectToAction("Index");
                }
                return View("SatisGetir", deger);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SatisGuncelle(SatisHareket p)
        {
            using (var c = new Context())
            {
                var deger = c.SatisHarekets.Find(p.Satisid);
                if (deger == null)
                {
                    TempData["Hata"] = "Satış kaydı bulunamadı!";
                    return RedirectToAction("Index");
                }
                deger.Cariid = p.Cariid;
                deger.Adet = p.Adet;
                deger.Fiyat = p.Fiyat;
                deger.Personelid = p.Personelid;
                deger.Tarih = p.Tarih;
                deger.ToplamTutar = p.ToplamTutar;
                deger.Urunid = p.Urunid;
                c.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult SatisDetay(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            using (var c = new Context())
            {
                var degerler = c.SatisHarekets.Include("Urun").Include("Cariler").Include("Personel").Where(x => x.Satisid == id.Value).ToList();
                return View(degerler);
            }
        }
    }
}