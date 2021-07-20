using DataAccesLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_PROJESI.Controllers
{
    public class IstatistikController : Controller
    {
        // GET: Istatistik

        private Context _db = new Context();

        public ActionResult Index()
        {
            ViewBag.KATEGORI_LENGHT = _db.Set<Category>().ToList().Count();

            ViewBag.BASLIK_YAZILIM_LENGHT = _db.Set<Heading>().Where(a => a.CategoryID == _db.Set<Category>().Where(b => b.CategoryName == "Yazılım").Select(c => c.CategoryID).FirstOrDefault()).ToList().Count;

            ViewBag.YAZAR_A_HARFI_ICEREN = _db.Set<Writer>().Where(a => a.WriterName.Contains("a")).ToList().Count;

            ViewBag.EN_FAZLA_BASLIK = _db.Set<Category>().Where(d => d.CategoryID == _db.Set<Heading>().GroupBy(a => a.CategoryID).Select(a => new
            {
                Name = a.Key,
                Count = a.Count()
            }).OrderByDescending(a => a.Count).Select(a => a.Name).FirstOrDefault()).Select(v => v.CategoryName).FirstOrDefault();

            ViewBag.KATEGORI_TRUE_FALSE_FARKI = _db.Set<Category>().Where(a => a.CategoryStatus == true).ToList().Count - _db.Set<Category>().Where(a => a.CategoryStatus == false).ToList().Count;

            return View();
        }
    }
}