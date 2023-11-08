using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetrakin.net.Entities
{
   public class paginaProyecto
    {
        public List<proyecto> Lista { get; set; }
        public int total { get; set; }
    }


    public class paginaPais
    {
        public List<pais> lista { get; set; }
        public int total { get; set; }
    }

}
