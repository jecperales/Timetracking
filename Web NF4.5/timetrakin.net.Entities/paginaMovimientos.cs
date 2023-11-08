using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetrakin.net.Entities
{
   public class paginaMovimientos
    {
        public List<movimientos> lista { get; set; }
        public int total { get; set; }
    }

   public class muestra_movimientos
    {
        [Key]
        public int id { get; set; }
        public int id_usuario { get; set; }
        public string nombre_usuario { get; set; }
        public string actividad { get; set; }
        public DateTime fecha_registro { get; set; }
        public string hora_inicio { get; set; }
        public string hora_fin { get; set; }
        public int id_cat_estatus { get; set; }
        public string nombre_estatus { get; set; }
        public string comentarios { get; set; }
        public string no_ticket { get; set; }
        public string localidad { get; set; }
        public string torre { get; set; }
        public string pais { get; set; }
        public string proyecto { get; set; }
    }
}
