using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace timetrakin.net.Controllers
{
    public class ManualesController : Controller
    {
        // GET: Manuales
        public ActionResult Index(string documeto)
        {
            if (Session["id"] != null)
            {
                var ruta = "/manuales/";
                if (documeto.Length == 0) {
                    ruta+=documeto= "Manualweb.pdf";
                }
                
                ViewBag.nombre = ruta += documeto;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Acount");
            }
            
        }
    }
}