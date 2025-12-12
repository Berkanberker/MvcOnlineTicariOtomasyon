using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.NewFolder1.siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class YapilacakController : Controller
    {
        public ActionResult Index()
        {
            using (var c = new Context())
            {
                ViewBag.d1 = c.Carilers.Count().ToString();
                ViewBag.d2 = c.Uruns.Count().ToString();
                ViewBag.d3 = c.Kategoris.Count().ToString();
                ViewBag.d4 = (from x in c.Carilers select x.CariSehri).Distinct().Count().ToString();

                var yapilacaklar = c.Yapilacaks.ToList();
                return View(yapilacaklar);
            }
        }
    }
}