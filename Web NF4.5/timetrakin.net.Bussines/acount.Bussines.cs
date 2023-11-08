using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timetrakin.net.Dao;
using timetrakin.net.Entities;

namespace timetrakin.net.Bussines
{
   public class acount
    {

        public datos_logeos login(usuarios usuario)
        {
            return new Dao.acount().login(usuario);
        }
    }
}
