using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using timetrakin.net.Entities;

namespace timetrakin.net.Controllers
{
    public class MovimientoController : Controller
    {
       JavaScriptSerializer serializer = new JavaScriptSerializer();
        // GET: Movimiento
        public ActionResult Index()
        {
            if (Session["id"] != null)
            {

                ViewBag.tipo_atencion = serializer.Serialize(new Bussines.administracion().getTipoatencion().
                    Select(x=> new {
                        id=x.id,label=x.nombre
                    }).ToList());

                ViewBag.torre= serializer.Serialize(new Bussines.administracion().getTipotorre().
                    Select(x => new {
                        id = x.id,
                        label = x.nombre
                    }).ToList());


              

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Acount");
            }
        }

        public ActionResult cambiar_estatus(int id_mov,int estatus)
        {
            int id_user = Int16.Parse(Session["id"].ToString());

           var result= new Bussines.movimiento().cambiar_estatus(id_mov,id_user, estatus);
            if (result.id!=0)
            {
                return Json(true);
            }
            else { return Json(false); }
          
        }

        public ActionResult registraMovimiento(movimientos mov)
        {
            mov.id_usuario = Int16.Parse(Session["id"].ToString());

            mov.fecha_registro_real = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");            

            var resul = new Bussines.movimiento().registraMovimiento(mov);
            if (resul.id!=0)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        public ActionResult paginaMovimientos(int pageIndex, int pageSize)
        {
            int id_usuario = Int16.Parse(Session["id"].ToString());
            var result = new Bussines.movimiento().paginaMovient(pageIndex, pageSize, id_usuario);
       
            return Json(result);
        }

        public ActionResult historialmovimientos()
        {
            if (Session["id"] != null)
            {

                ViewBag.proyecto = serializer.Serialize(new Bussines.administracion().getProyecto().
                   Select(x => new {
                       id = x.id,
                       label = x.nombre
                   }).ToList());

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Acount");
            }
        }

        public ActionResult buscarMovimientos(string fecha1,string fecha2,int trabajador,int proyect)
        {
            if (Session["id"] != null)
            {
                int id_usuario = 0;
                string ordena_usuarios = "";
                int idPerfil = int.Parse(Session["id_perfil"].ToString());
                if (idPerfil==1|| idPerfil==2)
                {
                    if (trabajador==0)
                    {
                        id_usuario = Int16.Parse(Session["id"].ToString());
                        var user_existentes = new Bussines.administracion().getUsuarios();
                        ordena_usuarios = user_existentes[0].id.ToString();
                        foreach (var item in user_existentes)
                        {
                            ordena_usuarios += ","+item.id;
                        }
                    }
                    else
                    {
                        id_usuario = trabajador;
                        ordena_usuarios += trabajador;
                    }
                   
                }
                else
                {
                     id_usuario = Int16.Parse(Session["id"].ToString());
                    ordena_usuarios += id_usuario;
                }
              
                var result = new Bussines.movimiento().buscarMovimientos(ordena_usuarios, fecha1,fecha2, proyect, idPerfil);
                return Json(result);
            }
            else
            {
                return RedirectToAction("Index", "Acount");
            }           
        }

        public ActionResult descargarMovimientos(string fecha1, string fecha2, int trabajador,int proyect)
        {
            if (Session["id"] != null)
            {
                int id_usuario = 0;
                string ordena_usuarios = "";
                int idPerfil = int.Parse(Session["id_perfil"].ToString());
                if (idPerfil == 1 || idPerfil == 2)
                {
                    if (trabajador == 0)
                    {
                        var user_existentes = new Bussines.administracion().getUsuarios();
                        ordena_usuarios = user_existentes[0].id.ToString();
                        foreach (var item in user_existentes)
                        {
                            ordena_usuarios += "," + item.id;
                        }
                    }
                    else
                    {
                        id_usuario = trabajador;
                        ordena_usuarios += trabajador;
                    }
                }
                else
                {
                    id_usuario = Int16.Parse(Session["id"].ToString());
                    ordena_usuarios += id_usuario;
                }

                var result = new Bussines.movimiento().buscarMovimientos(ordena_usuarios, fecha1, fecha2, proyect, idPerfil);

                //var datos_usuario = new Bussines.administracion().getUsuario(id_usuario);
                //var obtenr_pais = new Bussines.administracion().getPais(datos_usuario.id_pais);
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.AddWorksheet("Sheet1");

                    ws.Range("A1:E1").Merge();
                    ws.Cell(1, 1).Value = "Reporte time tracking LAC & LAS";
                    ws.Cell(1, 1).Style.Font.FontSize = 15;
                    ws.Cell(1, 1).Style.Font.FontColor = XLColor.Black;

                    //ws.Cell(3, 1).Value = "Pais:";
                    //ws.Cell(3, 1).Style.Font.Bold = true;
                    //ws.Cell(3, 1).Style.Font.FontColor = XLColor.Black;
                    //ws.Cell(3, 2).Value = obtenr_pais.nombre;
                    //ws.Cell(3, 2).Style.Font.Bold = true;
                    //ws.Cell(3, 2).Style.Font.FontColor = XLColor.Black;


                    //ws.Cell(4, 1).Value = "Localidad asignada:";
                    //ws.Cell(4, 1).Style.Font.Bold = true;
                    //ws.Cell(4, 1).Style.Font.FontColor = XLColor.Black;
                    //ws.Cell(5, 1).Value = "Nombre:";
                    //ws.Cell(5, 1).Style.Font.Bold = true;
                    //ws.Cell(5, 1).Style.Font.FontColor = XLColor.Black;
                    //ws.Cell(5, 2).Value = datos_usuario.nombre+" "+ datos_usuario.ap_paterno+" "+ datos_usuario.ap_materno;
                    //ws.Cell(5, 2).Style.Font.Bold = true;
                    //ws.Cell(5, 2).Style.Font.FontColor = XLColor.Black;

                    var rango_encabezados_tabla = ws.Range("A2:K2");
                    rango_encabezados_tabla.Style.Fill.BackgroundColor = XLColor.SkyBlue;
                    ws.Cell(2, 1).Value = "Nombre";
                    ws.Cell(2, 1).Style.Font.Bold = true;
                    ws.Cell(2, 1).Style.Font.FontColor = XLColor.Black;

                    ws.Cell(2, 2).Value = "Fecha";
                    ws.Cell(2, 2).Style.Font.Bold = true;
                    ws.Cell(2, 2).Style.Font.FontColor = XLColor.Black;

                    ws.Cell(2, 3).Value = "Actividad";
                    ws.Cell(2, 3).Style.Font.Bold = true;
                    ws.Cell(2, 3).Style.Font.FontColor = XLColor.Black;

                    ws.Cell(2, 4).Value = "Hora inicio";
                    ws.Cell(2, 4).Style.Font.Bold = true;
                    ws.Cell(2, 4).Style.Font.FontColor = XLColor.Black;

                    ws.Cell(2, 5).Value = "Hora finalizaíón";
                    ws.Cell(2, 5).Style.Font.Bold = true;
                    ws.Cell(2, 5).Style.Font.FontColor = XLColor.Black;

                    ws.Cell(2, 6).Value = "Tiempo";
                    ws.Cell(2, 6).Style.Font.Bold = true;
                    ws.Cell(2, 6).Style.Font.FontColor = XLColor.Black;

                    ws.Cell(2, 7).Value = "Comentarios";
                    ws.Cell(2, 7).Style.Font.Bold = true;
                    ws.Cell(2, 7).Style.Font.FontColor = XLColor.Black;

                    ws.Cell(2, 8).Value = "Localidad";
                    ws.Cell(2, 8).Style.Font.Bold = true;
                    ws.Cell(2, 8).Style.Font.FontColor = XLColor.Black;

                    ws.Cell(2, 9).Value = "Torre";
                    ws.Cell(2, 9).Style.Font.Bold = true;
                    ws.Cell(2, 9).Style.Font.FontColor = XLColor.Black;

                    ws.Cell(2, 10).Value = "País";
                    ws.Cell(2, 10).Style.Font.Bold = true;
                    ws.Cell(2, 10).Style.Font.FontColor = XLColor.Black;

                    ws.Cell(2, 11).Value = "Proyecto";
                    ws.Cell(2, 11).Style.Font.Bold = true;
                    ws.Cell(2, 11).Style.Font.FontColor = XLColor.Black;

                    int nRow = 3;
                    foreach (var item in result)
                    {
                        DateTime startTime =  DateTime.Parse(item.hora_inicio);
                        DateTime endTime =  DateTime.Parse(item.hora_fin);

                        TimeSpan span = endTime.Subtract(startTime);

                        ws.Cell(nRow, 1).Value = item.nombre_usuario;
                        ws.Cell(nRow, 2).Value = item.fecha_registro;
                        ws.Cell(nRow, 3).Value = item.actividad;
                        ws.Cell(nRow, 4).Value = item.hora_inicio;
                        ws.Cell(nRow, 5).Value = item.hora_fin;
                        ws.Cell(nRow, 6).Value = span.ToString();
                        ws.Cell(nRow, 7).Value = item.comentarios;
                        ws.Cell(nRow, 8).Value = item.localidad;
                        ws.Cell(nRow, 9).Value = item.torre;
                        ws.Cell(nRow, 10).Value = item.pais;
                        ws.Cell(nRow, 11).Value = item.proyecto;
                        //ws.Cell(nRow, 8).Value = item.no_ticket;
                        nRow++;
                    }

                        ws.Columns().AdjustToContents();


                    // wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {

                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Historial_movimientos" + DateTime.Today.ToShortDateString() + ".xlsx");

                    }
                }
               
            }
            else
            {
                return RedirectToAction("Index", "Acount");
            }
        }


        public ActionResult SearchPerson(string term)
        {
            var result = new Bussines.administracion().SearchPerson(term).Select(x=> new {
                PersonID =x.id,
                Name = x.nombre
            });

        return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SearchActividad(string term)
        {
            var result = new Bussines.administracion().SearchActividad(term).Select(x => new {
                PersonID = x.id,
                Name = x.nombre_actividad
            });

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult eliminar_registro(movimientos mov)
        {
            var existe = new Bussines.movimiento().get_movimiento(mov.id);
            bool status = false;
            if (existe!=null) {
                if (existe.fecha_registro.Date==DateTime.Now.Date)
                {
                    existe.id_cat_estatus = 3;
                    new Bussines.movimiento().eliminaMovimiento(existe);
                    status = true;
                }               
            }
            return Json(status);
        }

    }
}