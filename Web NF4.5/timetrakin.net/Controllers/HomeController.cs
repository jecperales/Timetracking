using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace timetrakin.net.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["id"]!=null)
            {
                int idUsuario = int.Parse(Session["id"].ToString());
                                
                ViewBag.actividades_totales = new Bussines.movimiento().get_total_movimiento(idUsuario, 1); ;
                ViewBag.pendientes = new Bussines.movimiento().get_total_movimiento(idUsuario, 4);
                ViewBag.cerrados = new Bussines.movimiento().get_total_movimiento(idUsuario, 2);
                ViewBag.cancelados = new Bussines.movimiento().get_total_movimiento(idUsuario, 3);

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Acount");
            }
           
        }

        public ActionResult About()
        {
            if (Session["id"] != null)
            {



                return View();
            }
            else
            {
                return RedirectToAction("Index", "Acount");
            }

        }

        public ActionResult Contact()
        {
            if (Session["id"] != null)
            {



                return View();
            }
            else
            {
                return RedirectToAction("Index", "Acount");
            }
        }
    }
}