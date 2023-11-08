using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timetrakin.net.Entities;

namespace timetrakin.net.Dao
{
   public class acount
    {
        public datos_logeos login(usuarios usuario)
        {
            datos_logeos log = new datos_logeos();
            using (Dbtimetrakin db = new Dbtimetrakin())
            {



                return (from u in db.usuarios
                        join p in db.perfil on u.id_perfil equals p.id
                        where u.correo == usuario.correo && u.contrasena == usuario.contrasena && u.estatus == 1
                        select new datos_logeos
                        {
                            id = u.id,
                            nombre = u.nombre + " " + u.ap_paterno + " " + u.ap_materno,
                            nombre_perfil = p.nombre,
                            id_perfil = p.id,
                            correo = u.correo
                        }).FirstOrDefault();

            }
        }
    }
}
