using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetrakin.net.Entities
{
   public class paginadousuarios
    {
        public List<pagi_user> Lista { get; set; }
        public int total { get; set; }
    }

    public class pagi_user
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string ap_paterno { get; set; }
        public string ap_materno { get; set; }
        public string correo { get; set; }
        public string contrasena { get; set; }
        public DateTime fecha_registro { get; set; }
        public DateTime fecha_modificacion { get; set; }
        public int id_proyecto { get; set; }
        public string nombre_proyecto { get; set; }
        public int id_perfil { get; set; }
        public string nombre_perfil { get; set; }
        public int estatus { get; set; }
        public string telefono { get; set; }
        public int id_pais { get; set; }
    }
}
