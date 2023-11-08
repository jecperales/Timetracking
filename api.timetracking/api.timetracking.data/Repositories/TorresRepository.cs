using api.timetracking.models;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.timetracking.data.Repositories
{
    public class TorresRepository : ITorresRepository
    {
        private readonly MySQLConfiguration _connectionString;

        public TorresRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        public async Task<cat_torres> FindById(int id)
        {
            var db = dbConnection();

            var sql = @"SELECT * FROM cat_torres WHERE id=@Id";

            return await db.QueryFirstOrDefaultAsync<cat_torres>(sql, new { Id = id });
        }

        public async Task<IEnumerable<cat_torres>> GetAllTorres()
        {
            var db = dbConnection();

            var sql = @"SELECT * FROM cat_torres";

            return await db.QueryAsync<cat_torres>(sql, new { });
        }
    }
}
