using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.NewFolder1.siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class PersonelController : Controller
    {
        // İzin verilen dosya uzantıları
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        private const int MaxFileSize = 2 * 1024 * 1024; // 2MB

        public ActionResult Index()
        {
            using (var c = new Context())
            {
                var degerler = c.Personels.Include("Departman").ToList();
                return View(degerler);
            }
        }

        [HttpGet]
        public ActionResult PersonelEkle()
        {
            using (var c = new Context())
            {
                List<SelectListItem> deger1 = (from x in c.Departmans
                                               select new SelectListItem
                                               {
                                                   Text = x.DepartmanAd,
                                                   Value = x.Departmanid.ToString()
                                               }).ToList();
                ViewBag.dgr1 = deger1;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PersonelEkle(Personel p)
        {
            using (var c = new Context())
            {
                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                {
                    var file = Request.Files[0];
                    string uzanti = Path.GetExtension(file.FileName).ToLower();

                    // Dosya uzantısı kontrolü
                    if (!_allowedExtensions.Contains(uzanti))
                    {
                        TempData["Hata"] = "Sadece resim dosyaları yükleyebilirsiniz! (jpg, jpeg, png, gif)";
                        return RedirectToAction("PersonelEkle");
                    }

                    // Dosya boyutu kontrolü
                    if (file.ContentLength > MaxFileSize)
                    {
                        TempData["Hata"] = "Dosya boyutu 2MB'dan büyük olamaz!";
                        return RedirectToAction("PersonelEkle");
                    }

                    // Benzersiz dosya adı oluştur
                    string dosyaadi = Guid.NewGuid().ToString() + uzanti;
                    string yol = "~/Image/" + dosyaadi;
                    file.SaveAs(Server.MapPath(yol));
                    p.PersonelGorsel = "/Image/" + dosyaadi;
                }
                c.Personels.Add(p);
                c.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult PersonelGetir(int id)
        {
            using (var c = new Context())
            {
                List<SelectListItem> deger1 = (from x in c.Departmans
                                               select new SelectListItem
                                               {
                                                   Text = x.DepartmanAd,
                                                   Value = x.Departmanid.ToString()
                                               }).ToList();
                ViewBag.dgr1 = deger1;
                var prs = c.Personels.Find(id);
                if (prs == null)
                {
                    TempData["Hata"] = "Personel bulunamadı!";
                    return RedirectToAction("Index");
                }
                return View(prs);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PersonelGuncelle(Personel p)
        {
            using (var c = new Context())
            {
                var prsn = c.Personels.Find(p.Personelid);
                if (prsn == null)
                {
                    TempData["Hata"] = "Personel bulunamadı!";
                    return RedirectToAction("Index");
                }

                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                {
                    var file = Request.Files[0];
                    string uzanti = Path.GetExtension(file.FileName).ToLower();

                    // Dosya uzantısı kontrolü
                    if (!_allowedExtensions.Contains(uzanti))
                    {
                        TempData["Hata"] = "Sadece resim dosyaları yükleyebilirsiniz! (jpg, jpeg, png, gif)";
                        return RedirectToAction("PersonelGetir", new { id = p.Personelid });
                    }

                    // Dosya boyutu kontrolü
                    if (file.ContentLength > MaxFileSize)
                    {
                        TempData["Hata"] = "Dosya boyutu 2MB'dan büyük olamaz!";
                        return RedirectToAction("PersonelGetir", new { id = p.Personelid });
                    }

                    // Benzersiz dosya adı oluştur
                    string dosyaadi = Guid.NewGuid().ToString() + uzanti;
                    string yol = "~/Image/" + dosyaadi;
                    file.SaveAs(Server.MapPath(yol));
                    prsn.PersonelGorsel = "/Image/" + dosyaadi;
                }

                prsn.PersonelAd = p.PersonelAd;
                prsn.PersonelSoyad = p.PersonelSoyad;
                prsn.Departmanid = p.Departmanid;
                c.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult PersonelListe()
        {
            using (var c = new Context())
            {
                var sorgu = c.Personels.Include("Departman").ToList();
                return View(sorgu);
            }
        }
    }
}
