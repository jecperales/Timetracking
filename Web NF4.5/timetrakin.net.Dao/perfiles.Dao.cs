using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timetrakin.net.Entities;

namespace timetrakin.net.Dao
{
   public class perfiles
    {

        public perfil registrar_perfil(perfil per)
        {
            using (Dbtimetrakin db=new Dbtimetrakin())
            {
                var existe = db.perfil.Where(x => x.id == per.id).FirstOrDefault();
                if (existe == null)
                {
                    db.perfil.Add(per);
                    db.SaveChanges();
                }
                else
                {
                    db.Entry(existe).CurrentValues.SetValues(per);
                    db.SaveChanges();
                }
            }
                return per;
        }

    }
}
