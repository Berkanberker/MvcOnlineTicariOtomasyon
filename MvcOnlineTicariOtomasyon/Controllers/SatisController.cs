using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.NewFolder1.siniflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {
     
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.SatisHarekets.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> deger1 = (from x in c.Uruns.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAd,
                                               Value = x.Urunid.ToString()
                                           }).ToList();


            List<SelectListItem> deger2 = (from x in c.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CarSoyad,
                                               Value = x.Cariid.ToString()
                                           }).ToList();

            List<SelectListItem> deger3 = (from x in c.Personels.ToList()
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
        [HttpPost]
        public ActionResult YeniSatis(SatisHareket s)
        {

            var urunFiyat = c.Uruns.Where(x => x.Urunid == s.Urunid).Select(z => z.SatisFiyat).FirstOrDefault();
            var urunStok = c.Uruns.Where(x => x.Urunid == s.Urunid).Select(z => z.Stok).FirstOrDefault();
            if (urunStok >= s.Adet)
            {
                urunStok = (short)(urunStok - s.Adet);

                var kalanStok = c.Uruns.Find(s.Urunid);
                kalanStok.Stok = urunStok;
                s.ToplamTutar = urunFiyat * s.Adet;
                s.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
                c.SatisHarekets.Add(s);
                c.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return RedirectPermanent("~/Urun/UrunGetir/" + s.Urunid);
            }
        }
        public ActionResult SatisGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Uruns.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.UrunAd,
                                              Value = x.Urunid.ToString()
                                          }).ToList();


            List<SelectListItem> deger2 = (from x in c.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CarSoyad,
                                               Value = x.Cariid.ToString()
                                           }).ToList();

            List<SelectListItem> deger3 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.Personelid.ToString()
                                           }).ToList();


            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;


            var deger = c.SatisHarekets.Find(id);
            return View("SatisGetir",deger);
        }
        public ActionResult SatisGuncelle(SatisHareket p)
        {
            var deger =c.SatisHarekets.Find(p.Satisid);
            deger.Cariid = p.Cariid;
            deger.Adet = p.Adet;
            deger.Fiyat =p.Fiyat;
            deger.Personelid =p.Personelid;
            deger.Tarih = p.Tarih;
            deger.ToplamTutar = p.ToplamTutar;
            deger.Urunid = p.Urunid;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SatisDetay(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.Satisid == id).ToList();
            return View(degerler);
        }
    }
}