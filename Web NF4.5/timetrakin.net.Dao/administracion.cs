using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timetrakin.net.Entities;

namespace timetrakin.net.Dao
{
   public class administracion
    {

        public usuarios registrar_usuario(usuarios usuario)
        {
            using (Dbtimetrakin db=new Dbtimetrakin())
            {
                var existe = db.usuarios.Where(x => x.id == usuario.id).FirstOrDefault();
                if (existe == null)
                {
                    db.usuarios.Add(usuario);
                    db.SaveChanges();
                }
                else
                {
                    db.Entry(existe).CurrentValues.SetValues(usuario);
                    db.SaveChanges();
                }
            }
            return usuario;
        }

        public List<cat_atencion> getTipoatencion()
        {
            using (Dbtimetrakin db=new Dbtimetrakin())
            {
                return db.cat_atencion.Where(x=> x.estatus==1).ToList();
            }
        }

        public List<cat_torres> getTipotorre()
        {
            using (Dbtimetrakin db=new Dbtimetrakin())
            {
                return db.cat_torres.Where(x=> x.estatus==1).ToList();
            }
        }

        public pais getPais(int p)
        {
            using (Dbtimetrakin db=new Dbtimetrakin())
            {
                return db.pais.Where(x=> x.id==p).FirstOrDefault();
            }
        }

        public pais regstraPais(pais p)
        {
            using (Dbtimetrakin db = new Dbtimetrakin())
            {
                var existe = db.pais.Where(x => x.id == p.id).FirstOrDefault();
                if (existe == null)
                {
                    db.pais.Add(p);
                    db.SaveChanges();
                }
                else
                {
                    db.Entry(existe).CurrentValues.SetValues(p);
                    db.SaveChanges();
                }
            }
            return p;
        }

        public bool eliminaToken(int id)
        {
            bool status = false;
            try {
                
                using (Dbtimetrakin db = new Dbtimetrakin())
                {
                    token t = db.token.Where(x=>x.id_usuario==id).FirstOrDefault();

                    db.token.Attach(t);
                    db.token.Remove(t);
                    db.SaveChanges();
                }
                status = true;
            } catch (Exception e1) { }
            
            return status;
        }

        public List<pais> getPaises()
        {
            using (Dbtimetrakin db=new Dbtimetrakin())
            {
                return db.pais.Where(x=> x.estatus==1).ToList();
            }
        }

        public List<pagi_user> paginausuarios(int pageIndex, int pageSize)
        {
            if (pageIndex > 0) { pageIndex = pageIndex - 1; }
            using (Dbtimetrakin db=new Dbtimetrakin())
            {
                return (from u in db.usuarios
                        join p in db.perfil on u.id_perfil equals p.id
                        join pro in db.proyecto on u.id_proyecto equals pro.id
                        where u.estatus==1
                        select new pagi_user {
                            id=u.id,
                            nombre=u.nombre,
                            ap_paterno=u.ap_paterno,
                            ap_materno=u.ap_materno,
                            contrasena=u.contrasena,
                            correo=u.correo,
                            fecha_registro=u.fecha_registro,
                            fecha_modificacion=u.fecha_modificacion,
                            estatus=u.estatus,
                            id_perfil=p.id,
                            nombre_perfil=p.nombre,
                            id_proyecto=pro.id,
                            nombre_proyecto=pro.nombre,
                            telefono=u.telefono,
                            id_pais=u.id_pais
                        }).OrderBy(x => x.id).ToList().Skip(pageSize * pageIndex).Take(pageSize).ToList();

                
            }
        }

        public List<cat_actividades> SearchActividad(string term)
        {
            using (Dbtimetrakin db = new Dbtimetrakin())
            {
                string query = "SELECT * FROM traking.cat_actividades where nombre_actividad like '%" + term + "%';";
                return db.cat_actividades.SqlQuery(query).ToList();
            }
        }

        public List<usuarios> getUsuarios()
        {
            using (Dbtimetrakin db=new Dbtimetrakin())
            {
                return db.usuarios.Where(x=> x.estatus==1).ToList();
            }
        }

        public int totalPaises()
        {
            using (Dbtimetrakin db=new Dbtimetrakin())
            {
                return db.pais.Where(x=> x.estatus==1).Count();
            }
        }

        public List<pais> listaPaises(int pageIndex, int pageSize)
        {
            if (pageIndex > 0) { pageIndex = pageIndex - 1; }

            using (Dbtimetrakin db = new Dbtimetrakin())
            {
                return db.pais.Where(x => x.estatus == 1).OrderBy(x => x.id).ToList().Skip(pageSize * pageIndex).Take(pageSize).ToList();
            }
        }

        public List<vista_usuarios> SearchPerson(string term)
        {
            using (Dbtimetrakin db=new Dbtimetrakin())
            {
                string query = @"select * from traking.vista_usuarios where nombre like '%"+term+"%'";

                return db.vista_usuarios.SqlQuery(query).ToList();
            }
        }

        public int toalProyectos()
        {
            using (Dbtimetrakin db=new Dbtimetrakin())
            {
                return db.proyecto.Where(x=> x.estatus==1).Count();
            }
        }

        public List<proyecto> listaproyectos(int pageIndex, int pageSize)
        {
            if (pageIndex > 0) { pageIndex = pageIndex - 1; }

            using (Dbtimetrakin db=new Dbtimetrakin())
            {
                return db.proyecto.Where(x=> x.estatus==1).OrderBy(x => x.id).ToList().Skip(pageSize * pageIndex).Take(pageSize).ToList();
            }
        }

        public proyecto registraProyecto(proyecto pro)
        {
            using (Dbtimetrakin db = new Dbtimetrakin())
            {
                var existe = db.proyecto.Where(x => x.id == pro.id).FirstOrDefault();
                if (existe == null)
                {
                    db.proyecto.Add(pro);
                    db.SaveChanges();
                }
                else
                {
                    db.Entry(existe).CurrentValues.SetValues(pro);
                    db.SaveChanges();
                }
            }
            return pro;
        }

        public usuarios getUsuario(int id)
        {
            using (Dbtimetrakin db=new Dbtimetrakin())
            {
                return db.usuarios.Where(x=> x.id==id).FirstOrDefault();
            }
        }

        public int total_usuarios()
        {
            using (Dbtimetrakin db=new Dbtimetrakin())
            {
                return db.usuarios.Where(x=> x.estatus==1).Count();
            }
        }

        public List<proyecto> getProyecto()
        {
            using (Dbtimetrakin db=new Dbtimetrakin())
            {
                return db.proyecto.Where(x=> x.estatus==1).ToList();
            }
        }

        public List<perfil> getPerfil()
        {
            using (Dbtimetrakin db=new Dbtimetrakin())
            {
               return db.perfil.Where(x=> x.estatus==1).ToList();
            }
        }
    }
}
