using api.timetracking.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.timetracking.data.Repositories
{
    public interface ITipoAtencionRepository
    {
        Task<IEnumerable<cat_atencion>> GetAllTipoAtencion();
        Task<cat_atencion> FindById(int id);
    }
}
