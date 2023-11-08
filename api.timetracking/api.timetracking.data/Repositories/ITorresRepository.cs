using api.timetracking.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.timetracking.data.Repositories
{
    public interface ITorresRepository
    {
        Task<IEnumerable<cat_torres>> GetAllTorres();
        Task<cat_torres> FindById(int id);
       
    }
}
