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
    public class istatistikController : Controller
    {
        public ActionResult Index()
        {
            using (var c = new Context())
            {
                ViewBag.d1 = c.Carilers.Count().ToString();
                ViewBag.d2 = c.Uruns.Count().ToString();
                ViewBag.d3 = c.Personels.Count().ToString();
                ViewBag.d4 = c.Kategoris.Count().ToString();
                ViewBag.d5 = (c.Uruns.Sum(x => (int?)x.Stok) ?? 0).ToString();
                ViewBag.d6 = (from x in c.Uruns select x.Marka).Distinct().Count().ToString();
                ViewBag.d7 = c.Uruns.Count(x => x.Stok <= 20).ToString();
                ViewBag.d8 = (from x in c.Uruns orderby x.SatisFiyat descending select x.UrunAd).FirstOrDefault();
                ViewBag.d9 = (from x in c.Uruns orderby x.SatisFiyat ascending select x.UrunAd).FirstOrDefault();
                ViewBag.d10 = c.Uruns.Count(x => x.UrunAd == "Buzdolabı").ToString();
                ViewBag.d11 = c.Uruns.Count(x => x.UrunAd == "Laptop").ToString();
                ViewBag.d12 = c.Uruns.GroupBy(x => x.Marka).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
                ViewBag.d13 = c.Uruns.Where(u => u.Urunid == (c.SatisHarekets.GroupBy(x => x.Urunid).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault())).Select(k => k.UrunAd).FirstOrDefault();
                ViewBag.d14 = (c.SatisHarekets.Sum(x => (decimal?)x.ToplamTutar) ?? 0).ToString();
                DateTime bugun = DateTime.Today;
                ViewBag.d15 = c.SatisHarekets.Count(x => x.Tarih == bugun).ToString();
                ViewBag.d16 = (c.SatisHarekets.Where(x => x.Tarih == bugun).Sum(y => (decimal?)y.ToplamTutar) ?? 0).ToString();
                return View();
            }
        }

        public ActionResult KolayTablolar()
        {
            using (var c = new Context())
            {
                var sorgu = from x in c.Carilers
                            group x by x.CariSehri into g
                            select new SinifGrup
                            {
                                Sehir = g.Key,
                                Sayi = g.Count()
                            };
                return View(sorgu.ToList());
            }
        }

        public PartialViewResult Partial1()
        {
            using (var c = new Context())
            {
                var sorgu2 = from x in c.Personels
                             group x by x.Departman.DepartmanAd into g
                             select new SinifGrup2
                             {
                                 Departman = g.Key,
                                 Sayi = g.Count()
                             };
                return PartialView(sorgu2.ToList());
            }
        }

        public PartialViewResult Partial2()
        {
            using (var c = new Context())
            {
                var sorgu = c.Carilers.ToList();
                return PartialView(sorgu);
            }
        }

        public PartialViewResult Partial3()
        {
            using (var c = new Context())
            {
                var sorgu = c.Uruns.ToList();
                return PartialView(sorgu);
            }
        }

        public PartialViewResult Partial4()
        {
            using (var c = new Context())
            {
                var sorgu = from x in c.Uruns
                            group x by x.Marka into g
                            select new SinifGrup3
                            {
                                marka = g.Key,
                                sayi = g.Count()
                            };
                return PartialView(sorgu.ToList());
            }
        }
    }
}