using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.NewFolder1.siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class GaleriController : Controller
    {
        public ActionResult Index()
        {
            using (var c = new Context())
            {
                var degerler = c.Uruns.ToList();
                return View(degerler);
            }
        }
    }
}