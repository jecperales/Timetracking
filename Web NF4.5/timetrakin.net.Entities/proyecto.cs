using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetrakin.net.Entities
{
   public class proyecto
    {
        public int id { get; set; }
        public string  nombre  { get; set; }
        public DateTime fecha_registro  { get; set; }
        public int estatus  { get; set; }

    }
}
