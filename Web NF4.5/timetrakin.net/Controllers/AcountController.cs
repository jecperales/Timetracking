using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using timetrakin.net.Entities;

namespace timetrakin.net.Controllers
{
    public class AcountController : Controller
    {
        // GET: Acount
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult perfil()
        {
            if (Session["id"] != null)
            {
                int idUsuario = int.Parse(Session["id"].ToString());
                var result = new Bussines.administracion().getUsuario(idUsuario);
                
                ViewBag.correo = result.correo;
                ViewBag.contra = result.contrasena;
                ViewBag.perfil = Session["nombre_perfil"].ToString();

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Acount");
            }
        }

        public ActionResult actualizaDatos(string correo,string psw)
        {
            int idUsuario = int.Parse(Session["id"].ToString());
            var existe = new Bussines.administracion().getUsuario(idUsuario);
            existe.correo = existe.correo != correo ? correo : existe.correo;
            existe.contrasena = existe.contrasena != psw ? psw : existe.contrasena;

            var resul= new Bussines.administracion().registra_usuarios(existe);
            if (resul.id!=0)
            {
                // se elimina el token
                // es espera confimracion de liberacion por parte de app movil
                //new Bussines.administracion().eliminaToken(resul.id);

                return Json(true);
            }
            else { return Json(false); }
            
        }

        public ActionResult login(string usuario,string psw)
        {
            usuarios user = new usuarios();
            user.contrasena = psw;
            user.correo = usuario;
            var resultado = new Bussines.acount().login(user);
            
            if(resultado!=null)
            {
                Session["id"] = resultado.id;
                Session["nambre"] = resultado.nombre;
                Session["id_perfil"] = resultado.id_perfil;
                Session["nombre_perfil"] = resultado.nombre_perfil;
                Session["correo"] = resultado.correo;

                if (resultado.id_perfil == 4 || resultado.id_perfil == 5 || resultado.id_perfil == 6)
                {
                    return RedirectToAction("historialmovimientos", "Movimiento");
                }
                else 
                {
                    return RedirectToAction("Index", "Movimiento");
                }                
            }
            else
            {
                return RedirectToAction("Index", "Acount");
            }
           
        }

        public ActionResult logout()
        {
            Session["id"] = null;
            Session["nambre"] = null;
            Session["id_perfil"] = null;
            Session["nombre_perfil"] = null;
            return RedirectToAction("Index", "Acount");
        }


    }
}