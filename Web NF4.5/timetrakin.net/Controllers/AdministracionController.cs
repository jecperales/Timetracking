using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using timetrakin.net.Entities;

namespace timetrakin.net.Controllers
{
    public class AdministracionController : Controller
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        // GET: Administracion

        public ActionResult altaPais()
        {
            if (Session["id"] != null)
            {
                ViewBag.paises = serializer.Serialize(new Bussines.administracion().getPaises().Select(                    
                    x=> new {
                        id=x.id,
                        label=x.nombre
                    }).ToList());

               

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Acount");
            }
        }

        public ActionResult eliminaPais(int id)
        {
            var existe = new Bussines.administracion().getPais(id);
            if (existe.id!=0)
            {
                existe.estatus = 0;
                new Bussines.administracion().regstraPais(existe);
                return Json(true);
            }
            else { return Json(false); }
        }

        public ActionResult registrarPais(pais pais)
        {
            var resul = new Bussines.administracion().regstraPais(pais);
            if (resul.id!=0)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
            
        }

        public ActionResult alta_usuarios()
        {
            if (Session["id"]!=null)
            {
                int idPerfil = int.Parse(Session["id_perfil"].ToString());                            

                ViewBag.proyecto = serializer.Serialize(new Bussines.administracion()
                                .getProyecto().Select(x => new {
                                    id = x.id,
                                    label = x.nombre
                                }).ToList());

                if (idPerfil==1)
                {
                    ViewBag.perfil = serializer.Serialize(new Bussines.administracion().getPerfil().Select(x => new {
                        id = x.id,
                        label = x.nombre
                    }).ToList());
                }
                else
                {
                    ViewBag.perfil = serializer.Serialize(new Bussines.administracion()
                        .getPerfil().Where(x=> x.id!=1).Select(x => new {
                        id = x.id,
                        label = x.nombre
                    }).ToList());
                }

                ViewBag.pais= serializer.Serialize(new Bussines.administracion().getPaises().Select(x => new {
                    id = x.id,
                    label = x.nombre
                }).ToList());

                return View();
            }
            else
            {
                return RedirectToAction("Index","Acount");
            }
           
        }

        public ActionResult registra_usuario(usuarios user)
        {
            var resul = new Bussines.administracion().registra_usuarios(user);
            bool estatus = false;
            if (resul.id!=0)
            {
                estatus = true;
            }
            return Json(estatus);
        }

        public ActionResult elimina_usuarios(int id)
        {
            var resu = new Bussines.administracion().getUsuario(id);
            bool estattus = false;
            if (resu.id!=0)
            {
                resu.estatus = 0;
                resu.fecha_modificacion = DateTime.Now;
                var resul = new Bussines.administracion().registra_usuarios(resu);
                estattus = true;
            }
            return Json(estattus);
        }

        public ActionResult paginado_usuarios(int pageIndex,int pageSize)
        {
            if (Session["id"] != null)
            {
                int idPerfil = int.Parse(Session["id_perfil"].ToString());
                paginadousuarios resultado = new paginadousuarios();
                if (idPerfil==1)
                {
                    resultado = new Bussines.administracion().pagina_usuario(pageIndex, pageSize);
                }
                else
                {
                    resultado = new Bussines.administracion().pagina_usuario(pageIndex, pageSize);
                    resultado.Lista = resultado.Lista.Where(x => x.id_perfil != 1).ToList();
                }
               
                return Json(resultado);
            }
            else
            {
                return RedirectToAction("Index", "Acount");
            }
           
        }



        public ActionResult alta_proyecto()
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

        public ActionResult registraProyecto(proyecto pro)
        {
            var result = new Bussines.administracion().registraProyecto(pro);
            if (result.id!=0)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
          
        }

        public ActionResult eliminaProyecto(int id)
        {
            var resul = new Bussines.administracion().getProyecto().Where(x=> x.id==id).FirstOrDefault();
            if (resul.id!=0)
            {
                resul.estatus = 0;
                new Bussines.administracion().registraProyecto(resul);
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        public ActionResult paginaProyectos(int pageIndex, int pageSize)
        {
            if (Session["id"] != null)
            {
                int idPerfil = int.Parse(Session["id_perfil"].ToString());
                paginaProyecto result = new paginaProyecto();
                result = new Bussines.administracion().paginaProyectos(pageIndex, pageSize);
               
                return Json(result);
            }
            else
            {
                return RedirectToAction("Index", "Acount");
            }
               
        }

        public ActionResult paginado_paises(int pageIndex, int pageSize)
        {
            var result = new Bussines.administracion().paginaPaises(pageIndex, pageSize);
            return Json(result);
        }



    }
}