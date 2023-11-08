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
    public class MovimientoRepository : IMovimientoRepository
    {
        private readonly MySQLConfiguration _connectionString;

        public MovimientoRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }



        public async Task<bool> DeleteMovimiento(int id)
        {
            var db = dbConnection();

            var sql = @"DELETE FROM movimientos WHERE id=@Id";

            var result = await db.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        public async Task<IEnumerable<movimientos>> FindAllByDate(int id_usuario, string f1, string f2, int id_proyecto, int id_perfil)
        {
            var db = dbConnection();


            string sqlUsuario = @"SELECT m.id,m.id_usuario,concat(u.nombre,' ',u.ap_paterno,' ',u.ap_materno) as nombre_usuario, 
                    m.actividad,m.fecha_registro,m.hora_inicio,m.hora_fin,c.nombre,m.id_cat_estatus,c.nombre as nombre_estatus,m.comentarios,m.no_ticket,
                    m.localidad,ct.nombre as torre,p.nombre as Pais,pr.nombre as proyecto
                    FROM traking.movimientos m
                    join traking.usuarios u on m.id_usuario=u.id
                    join traking.cat_estatus c on m.id_cat_estatus=c.id
                    join traking.cat_torres ct on m.id_cat_torres=ct.id
                    join traking.pais p on u.id_pais=p.id
                    join traking.proyecto pr on u.id_proyecto=pr.id
                    where m.id_usuario in (@Id) and  date_format( m.fecha_registro,'%Y-%m-%d') between @f1 and @f2";

            string sqlReportingAdmin = @"SELECT
                                            m.id,m.id_usuario,concat(u.nombre, ' ', u.ap_paterno, ' ', u.ap_materno) as nombre_usuario,m.actividad,m.fecha_registro,m.hora_inicio,
                                            m.hora_fin,c.nombre,m.id_cat_estatus,c.nombre as nombre_estatus,m.comentarios,m.no_ticket,m.localidad,ct.nombre as torre,p.nombre as Pais,
                                            pr.nombre as proyecto
                                            FROM traking.movimientos m
                                                join traking.usuarios u on m.id_usuario = u.id
                                                join traking.cat_estatus c on m.id_cat_estatus = c.id
                                                join traking.cat_torres ct on m.id_cat_torres = ct.id
                                                join traking.pais p on u.id_pais = p.id
                                                join traking.proyecto pr on u.id_proyecto = pr.id
                                            where 
                                                date_format( m.fecha_registro,'%Y-%m-%d') between @f1 AND @f2
                                                AND u.estatus = 1
                                                AND pr.nombre = 'Bimbo'                                                
                                        ORDER BY u.nombre, m.fecha_registro DESC;";

            string sqlSudamerica1 = @"SELECT
                                            m.id,m.id_usuario,concat(u.nombre, ' ', u.ap_paterno, ' ', u.ap_materno) as nombre_usuario,m.actividad,m.fecha_registro,m.hora_inicio,
                                            m.hora_fin,c.nombre,m.id_cat_estatus,c.nombre as nombre_estatus,m.comentarios,m.no_ticket,m.localidad,ct.nombre as torre,p.nombre as Pais,
                                            pr.nombre as proyecto
                                            FROM traking.movimientos m
                                                join traking.usuarios u on m.id_usuario = u.id
                                                join traking.cat_estatus c on m.id_cat_estatus = c.id
                                                join traking.cat_torres ct on m.id_cat_torres = ct.id
                                                join traking.pais p on u.id_pais = p.id
                                                join traking.proyecto pr on u.id_proyecto = pr.id
                                            where 
                                                date_format( m.fecha_registro,'%Y-%m-%d') between @f1 AND @f2
                                                AND u.estatus = 1
                                                AND pr.nombre = 'Bimbo'
                                                AND p.nombre in ('Argentina','Paraguay','Chile','Perú')
                                        ORDER BY u.nombre, m.fecha_registro DESC;";

            string sqlSudamerica2 = @"SELECT
                                            m.id,m.id_usuario,concat(u.nombre, ' ', u.ap_paterno, ' ', u.ap_materno) as nombre_usuario,m.actividad,m.fecha_registro,m.hora_inicio,
                                            m.hora_fin,c.nombre,m.id_cat_estatus,c.nombre as nombre_estatus,m.comentarios,m.no_ticket,m.localidad,ct.nombre as torre,p.nombre as Pais,
                                            pr.nombre as proyecto
                                            FROM traking.movimientos m
                                                join traking.usuarios u on m.id_usuario = u.id
                                                join traking.cat_estatus c on m.id_cat_estatus = c.id
                                                join traking.cat_torres ct on m.id_cat_torres = ct.id
                                                join traking.pais p on u.id_pais = p.id
                                                join traking.proyecto pr on u.id_proyecto = pr.id
                                            where 
                                                date_format( m.fecha_registro,'%Y-%m-%d') between @f1 AND @f2
                                                AND u.estatus = 1
                                                AND pr.nombre = 'Bimbo'
                                                AND p.nombre in ('Colombia','Venezuela','Ecuador')
                                        ORDER BY u.nombre, m.fecha_registro DESC;";

            if (id_proyecto != 0)
            {
                sqlUsuario += " and pr.id = @id_proy";
            }

            switch (id_perfil)
            {                
                //ADMIN & LAS/LAC
                case 1 or 4:
                    return await db.QueryAsync<movimientos>(sqlReportingAdmin, new { f1 = f1, f2 = f2});                    

                //LAS
                case 5:
                    return await db.QueryAsync<movimientos>(sqlSudamerica1, new { f1 = f1, f2 = f2 });                    

                //LAC
                case 6:
                    return await db.QueryAsync<movimientos>(sqlSudamerica2, new { f1 = f1, f2 = f2 });                    

                //SUPERVISOR & USUARIO
                default:
                    if (id_proyecto != 0)
                    {
                        return await db.QueryAsync<movimientos>(sqlUsuario, new { Id = id_usuario, f1 = f1, f2 = f2, id_proy = id_proyecto });
                    }
                    else
                    {
                        return await db.QueryAsync<movimientos>(sqlUsuario, new { Id = id_usuario, f1 = f1, f2 = f2 });
                    }                                        
            }            
        }

        public async Task<IEnumerable<movimientos>> FindById(int id)
        {
            var db = dbConnection();

            var sql = @"SELECT * FROM traking.movimientos                     
                        where id_usuario= @Id and date_format(fecha_registro_real,'%Y-%m-%d') = date_format(NOW(),'%Y-%m-%d') ORDER BY id";

            return await db.QueryAsync<movimientos>(sql, new { Id = id });
        }

        public async Task<bool> InsertMovimiento(movimientos mov)
        {
            var db = dbConnection();

            var sql = @"INSERT INTO movimientos 
                            (id_usuario, actividad, fecha_registro, hora_inicio, hora_fin, id_cat_estatus, 
                            comentarios, no_ticket, id_tipo_atencion, id_cat_torres, localidad, fecha_registro_real)                             
                        VALUES
                            (@id_usuario, @actividad, @fecha_registro, @hora_inicio, @hora_fin, @id_cat_estatus, 
                            @comentarios, @no_ticket, @id_tipo_atencion, @id_cat_torres ,@localidad, @fecha_registro_real)";

            var result = await db.ExecuteAsync(sql,
                                               new
                                               {
                                                   mov.id_usuario, mov.actividad, mov.fecha_registro, mov.hora_inicio, mov.hora_fin,
                                                   mov.id_cat_estatus, mov.comentarios, mov.no_ticket, mov.id_tipo_atencion, mov.id_cat_torres,
                                                   mov.localidad, mov.fecha_registro_real
                                               });
            return result > 0;
        }

        public async Task<bool> UpdateMovimiento(movimientos  mov)
        {
            var db = dbConnection();

            var sql = @"UPDATE movimientos 
                        SET
                            id_usuario = @id_usuario, actividad = @actividad,
                            fecha_registro = @fecha_registro, hora_inicio = @hora_inicio, hora_fin = @hora_fin,
                            id_cat_estatus = @id_cat_estatus, comentarios = @comentarios, 
                            id_tipo_atencion = @id_tipo_atencion, id_cat_torres = @id_cat_torres,
                            localidad = @localidad
                        WHERE id = @Id";

            var result = await db.ExecuteAsync(sql,
                                               new
                                               {
                                                   Id = mov.id, mov.id_usuario, mov.actividad, mov.fecha_registro, mov.hora_inicio, mov.hora_fin,
                                                   mov.id_cat_estatus, mov.comentarios, mov.id_tipo_atencion, mov.id_cat_torres,
                                                   mov.localidad
                                               });
            return result > 0;
        }
        
    }
}
