using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetrakin.net.Entities
{
   public class token
    {
        public int id { get; set; }
        public string token_id { get; set; }
        public int estatus { get; set; }
        public int id_usuario { get; set; }
        public DateTime fecha_registro{ get; set; }
        public DateTime fecha_modificaccion{ get; set; }
        public int sesion_activa{ get; set; }
    }
}
