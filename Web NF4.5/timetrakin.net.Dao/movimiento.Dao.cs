using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timetrakin.net.Entities;

namespace timetrakin.net.Dao
{
   public class movimiento
    {

        public Entities.movimientos getMovimiento(int id)
        {
            using (Dbtimetrakin db=new Dbtimetrakin())
            {
                return db.movimientos.Where(x=> x.id==id).FirstOrDefault();
            }
        }

        public Entities.movimientos registraMovimiento(Entities.movimientos mov)
        {
            using (Dbtimetrakin db = new Dbtimetrakin())
            {
                var existe = db.movimientos.Where(x => x.id == mov.id).FirstOrDefault();
                if (existe == null)
                {
                    db.movimientos.Add(mov);
                    db.SaveChanges();
                }
                else
                {
                    db.Entry(existe).CurrentValues.SetValues(mov);
                    db.SaveChanges();
                }
            }
            return mov;
        }

       

        public int get_total_movimiento(int idUsuario, int movimiento)
        {
            using (Dbtimetrakin db=new Dbtimetrakin())
            {
                return db.movimientos.Where(x=> x.id_usuario==idUsuario && x.id_cat_estatus==movimiento).Count();
            }
        }

        public Entities.movimientos get_movimiento(int id)
        {
            using (Dbtimetrakin db=new Dbtimetrakin())
            {
                return db.movimientos.Where(x=> x.id==id).FirstOrDefault();
            }
        }
    }
}
