using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timetrakin.net.Entities;

namespace timetrakin.net.Bussines
{
    public class administracion
    {

        public usuarios registra_usuarios(usuarios user)
        {
            return new Dao.administracion().registrar_usuario(user);
        }

        public List<perfil> getPerfil()
        {
            return new Dao.administracion().getPerfil();
        }

        public List<proyecto> getProyecto()
        {
            return new Dao.administracion().getProyecto();
        }

        public List<cat_atencion> getTipoatencion()
        {
            return new Dao.administracion().getTipoatencion();
        }

        public pais regstraPais(pais p)
        {
            return new Dao.administracion().regstraPais(p);
        }

        public List<cat_torres> getTipotorre()
        {
            return new Dao.administracion().getTipotorre();
        }

        public pais getPais(int p)
        {
            return new Dao.administracion().getPais(p);
        }

        public List<pais> getPaises()
        {
            return new Dao.administracion().getPaises();
        }

        public paginadousuarios pagina_usuario(int pageIndex, int pageSize)
        {
            paginadousuarios user = new paginadousuarios();
            user.Lista = new Dao.administracion().paginausuarios( pageIndex,  pageSize);
            user.total = new Dao.administracion().total_usuarios();
            return user;
        }

        public usuarios getUsuario(int id)
        {
            return new Dao.administracion().getUsuario(id);
        }

        public bool eliminaToken(int id)
        {
            return new Dao.administracion().eliminaToken(id);
        }

        public proyecto registraProyecto(proyecto pro)
        {
            return new Dao.administracion().registraProyecto(pro);
        }

        public paginaProyecto paginaProyectos(int pageIndex, int pageSize)
        {
            paginaProyecto pro = new paginaProyecto();
            pro.Lista = new Dao.administracion().listaproyectos( pageIndex,  pageSize);
            pro.total = new Dao.administracion().toalProyectos();
            return pro;
        }

        public List<vista_usuarios> SearchPerson(string term)
        {
            return new Dao.administracion().SearchPerson(term);
        }

        public paginaPais paginaPaises(int pageIndex, int pageSize)
        {
            paginaPais p = new paginaPais();
            p.lista = new Dao.administracion().listaPaises( pageIndex,  pageSize);
            p.total = new Dao.administracion().totalPaises();
            return p;
        }

        public List<usuarios> getUsuarios()
        {
            return new Dao.administracion().getUsuarios();
        }

        public List<cat_actividades> SearchActividad(string term)
        {
            return new Dao.administracion().SearchActividad(term);
        }
    }
}
