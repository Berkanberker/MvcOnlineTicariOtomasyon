using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.NewFolder1.siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class UrunDetayController : Controller
    {
        public ActionResult Index()
        {
            using (var c = new Context())
            {
                Class1 cs = new Class1();
                cs.Deger1 = c.Uruns.Where(x => x.Urunid == 1).ToList();
                cs.Deger2 = c.Detays.Where(y => y.DetayID == 1).ToList();
                return View(cs);
            }
        }
    }
}