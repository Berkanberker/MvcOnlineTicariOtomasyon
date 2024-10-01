using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.NewFolder1.siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers { 

public class GaleriController : Controller
{
    // GET: Galeri
    Context c = new Context();
    public ActionResult Index()
    {
        var degerler = c.Uruns.ToArray().ToList();
        return View(degerler);
    }
  }
}