using api.timetracking.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.timetracking.data.Repositories
{
    public interface IMovimientoRepository
    {
        //Task<IEnumerable<movimientos>> GetAllMovimientos();
        Task<IEnumerable<movimientos>> FindById(int id);
        Task<IEnumerable<movimientos>> FindAllByDate(int id_usuario, string f1, string f2, int id_proyecto, int id_perfil);
        Task<bool> InsertMovimiento(movimientos movimiento);
        Task<bool> UpdateMovimiento(movimientos movimiento);
        Task<bool> DeleteMovimiento(int id);
    }
}
