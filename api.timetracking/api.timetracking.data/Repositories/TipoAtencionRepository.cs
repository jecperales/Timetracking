using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.timetracking.models;
using api.timetracking.data;
using MySql.Data.MySqlClient;
using Dapper;

namespace api.timetracking.data.Repositories
{
    public class TipoAtencionRepository : ITipoAtencionRepository
    {
        private readonly MySQLConfiguration _connectionString;
        public TipoAtencionRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;   
        }

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        public async Task<cat_atencion> FindById(int id)
        {
            var db = dbConnection();

            var sql = @"SELECT * FROM cat_atencion WHERE id=@Id";

            return await db.QueryFirstOrDefaultAsync<cat_atencion>(sql, new { Id = id });
        }

        public async Task<IEnumerable<cat_atencion>> GetAllTipoAtencion()
        {
            var db = dbConnection();

            var sql = @"SELECT * FROM cat_atencion";

            return await db.QueryAsync<cat_atencion>(sql, new { });
        }
    }
}
