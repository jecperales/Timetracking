using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.timetracking.models
{
    public class movimientos
    {
        public int id { get; set; }
        public int id_usuario { get; set; }
        public string actividad { get; set; }
        public DateTime fecha_registro { get; set; }
        public string hora_inicio { get; set; }
        public string hora_fin { get; set; }
        public int id_cat_estatus { get; set; }
        public string comentarios { get; set; }
        public string? no_ticket { get; set; }
        public int id_cat_torres { get; set; }
        public int id_tipo_atencion { get; set; }
        public string localidad { get; set; }
        public string fecha_registro_real { get; set; }
    }
}
