using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timetrakin.net.Entities;

namespace timetrakin.net.Bussines
{
    public class movimiento
    {
        public Entities.movimientos registraMovimiento(movimientos mov)
        {
            return new Dao.movimiento().registraMovimiento(mov);
        }

        public paginaMovimientos paginaMovient(int pageIndex, int pageSize,int id_usuario)
        {
            paginaMovimientos pag = new paginaMovimientos();
            pag.lista = new Dao.movimientos().pagina_movi(pageIndex, pageSize, id_usuario);
            pag.total = new Dao.movimientos().get_total_mov_usuario(id_usuario);
            return pag;
        }

        public int get_total_movimiento(int idUsuario, int movimiento)
        {
            return new Dao.movimiento().get_total_movimiento( idUsuario,  movimiento);
        }

        public movimientos cambiar_estatus(int id_mov, int id_user,int estatus)
        {
            movimientos m = new movimientos();

            var existe = new Dao.movimiento().get_movimiento(id_mov);
            if (existe.id!=0)
            {
                existe.id_cat_estatus = estatus;

               m = registraMovimiento(existe);
            }
            return m;
        }

        public Entities.movimientos get_movimiento(int id)
        {
            return new Dao.movimiento().get_movimiento(id);
        }
        public List<muestra_movimientos> buscarMovimientos(string id_usuario, string fecha1, string fecha2, int proyect, int id_perfil)
        {
            return new Dao.movimientos().buscarMovimientos(id_usuario,  fecha1,  fecha2,proyect, id_perfil);
        }

        public bool eliminaMovimiento(movimientos existe)
        {
            return new Dao.movimientos().eliminaMovimiento(existe);
        }
    }
}
