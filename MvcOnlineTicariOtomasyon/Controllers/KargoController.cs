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
    public class KargoController : Controller
    {
        public ActionResult Index()
        {
            using (var c = new Context())
            {
                var degerler = c.Kargos.OrderByDescending(x => x.GonderimTarihi).ToList();
                return View(degerler);
            }
        }

        [HttpGet]
        public ActionResult YeniKargo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YeniKargo(Kargo p)
        {
            // TakipNo otomatik oluşturulduğu için validasyondan çıkarıyoruz
            ModelState.Remove("TakipNo");
            if (!ModelState.IsValid)
            {
                return View(p);
            }
            using (var c = new Context())
            {
                p.TakipNo = "KRG" + DateTime.Now.ToString("yyyyMMddHHmmss");
                p.GonderimTarihi = DateTime.Now;
                p.Durum = "Hazırlanıyor";
                c.Kargos.Add(p);
                c.SaveChanges();
                TempData["Basarili"] = "Kargo başarıyla oluşturuldu!";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult KargoGetir(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            using (var c = new Context())
            {
                var kargo = c.Kargos.Find(id.Value);
                if (kargo == null)
                {
                    TempData["Hata"] = "Kargo bulunamadı!";
                    return RedirectToAction("Index");
                }
                return View(kargo);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KargoGuncelle(Kargo p)
        {
            using (var c = new Context())
            {
                var kargo = c.Kargos.Find(p.KargoId);
                if (kargo == null)
                {
                    TempData["Hata"] = "Kargo bulunamadı!";
                    return RedirectToAction("Index");
                }
                kargo.AliciAd = p.AliciAd;
                kargo.AliciAdres = p.AliciAdres;
                kargo.AliciTelefon = p.AliciTelefon;
                kargo.AliciSehir = p.AliciSehir;
                kargo.Durum = p.Durum;
                if (p.Durum == "Teslim Edildi" && !kargo.TeslimTarihi.HasValue)
                {
                    kargo.TeslimTarihi = DateTime.Now;
                }
                c.SaveChanges();
                TempData["Basarili"] = "Kargo başarıyla güncellendi!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KargoSil(int id)
        {
            using (var c = new Context())
            {
                var kargo = c.Kargos.Find(id);
                if (kargo == null)
                {
                    TempData["Hata"] = "Kargo bulunamadı!";
                    return RedirectToAction("Index");
                }
                c.Kargos.Remove(kargo);
                c.SaveChanges();
                TempData["Basarili"] = "Kargo başarıyla silindi!";
                return RedirectToAction("Index");
            }
        }

        public ActionResult DurumGuncelle(int id, string durum)
        {
            using (var c = new Context())
            {
                var kargo = c.Kargos.Find(id);
                if (kargo != null)
                {
                    kargo.Durum = durum;
                    if (durum == "Teslim Edildi")
                    {
                        kargo.TeslimTarihi = DateTime.Now;
                    }
                    c.SaveChanges();
                    TempData["Basarili"] = "Kargo durumu güncellendi!";
                }
                return RedirectToAction("Index");
            }
        }
    }
}
