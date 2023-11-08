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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly MySQLConfiguration _connectionString;

        public UsuarioRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteUsuario(Usuario usuario)
        {
            var db = dbConnection();

            var sql = @"DELETE FROM usuarios WHERE id=@Id";

            var result = await db.ExecuteAsync(sql, new { Id = usuario.id});

            return result > 0;
        }

        public async Task<Usuario> FindById(int id)
        {
            var db = dbConnection();

            var sql = @"SELECT * FROM usuarios WHERE id=@Id";

            return await db.QueryFirstOrDefaultAsync<Usuario>(sql, new {Id = id });
        }

        public async Task<Usuario> Auth(string user, string password)
        {
            var db = dbConnection();

            var sql = "SELECT * FROM usuarios WHERE correo = @Uid AND contrasena = @Pwd";

            return await db.QueryFirstOrDefaultAsync<Usuario>(sql, new { Uid = user, Pwd = password });
        }

        public async Task<Usuario> FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuarios()
        {
            var db = dbConnection();

            var sql = @"SELECT * FROM usuarios";

            return await db.QueryAsync<Usuario>(sql, new { });  
        }

        public async Task<bool> InsertUsuario(Usuario user)
        {
            var db = dbConnection();

            var sql = @"INSERT INTO usuarios 
                            (nombre, ap_paterno, ap_materno, correo, contrasena, fecha_registro, fecha_modificacion, 
                            id_proyecto, id_perfil, estatus, telefono, id_pais)                             
                        VALUES
                            (@nombre, @ap_paterno, @ap_materno, @correo, @contrasena, @fecha_registro, @fecha_modificacion, 
                            @id_proyecto, @id_perfil, @estatus, @telefono, @id_pais)";

            var result = await db.ExecuteAsync(sql, 
                                               new { 
                                                   user.nombre,
                                                   user.ap_paterno,
                                                   user.ap_materno,
                                                   user.correo,
                                                   user.contrasena,
                                                   user.fecha_registro,
                                                   user.fecha_modificacion,
                                                   user.id_proyecto,
                                                   user.id_perfil,
                                                   user.estatus,
                                                   user.telefono,
                                                   user.id_pais
                                               });
            return result > 0;
        }

        public async Task<bool> UpdateUsuario(Usuario user)
        {
            var db = dbConnection();

            var sql = @"UPDATE usuarios 
                        SET
                            nombre =  @nombre, ap_paterno = @ap_paterno, ap_materno = @ap_materno,
                            correo = @correo, contrasena = @contrasena, fecha_registro = @fecha_registro,
                            fecha_modificacion = @fecha_modificacion, id_proyecto = @id_proyecto, 
                            id_perfil = @id_perfil, estatus = @estatus, telefono = @telefono, id_pais = @id_pais
                        WHERE id = @Id";

            var result = await db.ExecuteAsync(sql,
                                               new
                                               {
                                                   user.id,
                                                   user.nombre,
                                                   user.ap_paterno,
                                                   user.ap_materno,
                                                   user.correo,
                                                   user.contrasena,
                                                   user.fecha_registro,
                                                   user.fecha_modificacion,
                                                   user.id_proyecto,
                                                   user.id_perfil,
                                                   user.estatus,
                                                   user.telefono,
                                                   user.id_pais
                                               });
            return result > 0;
        }
    }
}
