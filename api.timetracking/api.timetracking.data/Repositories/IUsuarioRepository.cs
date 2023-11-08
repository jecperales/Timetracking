using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.timetracking.models;

namespace api.timetracking.data.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllUsuarios();
        Task<Usuario> FindById(int id);
        Task<Usuario> Auth(string user, string password);

        Task<Usuario> FindByName(string name);
        Task<bool> InsertUsuario(Usuario user);
        Task<bool> UpdateUsuario(Usuario usuer);
        Task<bool> DeleteUsuario(Usuario user);
    }
}
