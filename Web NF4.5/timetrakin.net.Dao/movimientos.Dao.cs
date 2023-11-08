using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timetrakin.net.Entities;

namespace timetrakin.net.Dao
{
   public class movimientos
    {
        public Entities.movimientos registro_movimiento(Entities.movimientos mov)
        {
            using (Dbtimetrakin db=new Dbtimetrakin())
            {
                var existe = db.movimientos.Where(x => x.id == mov.id).FirstOrDefault();
                if (existe == null)
                {
                    db.movimientos.Add(mov);
                    db.SaveChanges();
                }
                else
                {
                    db.Entry(existe).CurrentValues.SetValues(mov);
                    db.SaveChanges();
                }
            }
                return mov;
        }

        public List<muestra_movimientos> buscarMovimientos(string id_usuario, string fecha1, string fecha2, int proyect, int id_perfil)
        {
            string sql = "";
            sql = @"SELECT m.id,m.id_usuario,concat(u.nombre,' ',u.ap_paterno,' ',u.ap_materno) as nombre_usuario 
                    ,m.actividad,m.fecha_registro,m.hora_inicio,m.hora_fin,c.nombre,m.id_cat_estatus,c.nombre as nombre_estatus,m.comentarios,m.no_ticket
                    ,m.localidad,ct.nombre as torre,p.nombre as Pais,pr.nombre as proyecto
                    FROM traking.movimientos m
                    join traking.usuarios u on m.id_usuario=u.id
                    join traking.cat_estatus c on m.id_cat_estatus=c.id
                    join traking.cat_torres ct on m.id_cat_torres=ct.id
                    join traking.pais p on u.id_pais=p.id
                    join traking.proyecto pr on u.id_proyecto=pr.id
                    where m.id_usuario in (" + id_usuario+") and  date_format( m.fecha_registro,'%Y-%m-%d') between '"+fecha1+"' and '"+fecha2+"'";

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
                                                date_format( m.fecha_registro,'%Y-%m-%d') between '" + fecha1 + "' AND '" + fecha2 + "'" +
                                                @"AND u.estatus = 1
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
                                                date_format( m.fecha_registro,'%Y-%m-%d') between '" + fecha1 + "' AND '" + fecha2 + "'" +
                                                @" AND u.estatus = 1
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
                                                date_format( m.fecha_registro,'%Y-%m-%d') between '" + fecha1 + "' AND '" + fecha2 + "'" +
                                                @" AND u.estatus = 1
                                                AND pr.nombre = 'Bimbo'
                                                AND p.nombre in ('Colombia','Venezuela','Ecuador')
                                        ORDER BY u.nombre, m.fecha_registro DESC;";


            if (proyect!=0) 
            {
                sql += " and pr.id="+proyect+"";
            }

            try 
            {
                using (Dbtimetrakin db = new Dbtimetrakin())
                {
                    switch (id_perfil)
                    {
                        case 1:
                        case 4:
                            return db.muestra_movimientos.SqlQuery(sqlReportingAdmin).ToList();
                            
                        case 5:
                            return db.muestra_movimientos.SqlQuery(sqlSudamerica1).ToList();
                            
                        case 6:
                                return db.muestra_movimientos.SqlQuery(sqlSudamerica2).ToList();
                            
                        default:
                                return db.muestra_movimientos.SqlQuery(sql).ToList();
                    }
                 
                    //return db.muestra_movimientos.SqlQuery(sql).ToList();
                }
            } 
            catch (Exception e1)
            {
                return null;
            }
           
        }

        public bool eliminaMovimiento(Entities.movimientos existe)
        {
            bool stauts = false;
            try {
                using (Dbtimetrakin db = new Dbtimetrakin())
                {
                    db.movimientos.Attach(existe);
                    db.movimientos.Remove(existe);
                    db.SaveChanges();
                    stauts = true;
                }
            } catch (Exception e1) { }
            return stauts;
        }

        public int get_total_mov_usuario(int id_usuario)
        {
            using (Dbtimetrakin db=new Dbtimetrakin())
            {
                string sql = @"SELECT * FROM traking.movimientos
                             where id_usuario=" + id_usuario + " and date_format( fecha_registro,'%d/%m/%Y')='" + DateTime.Now.ToString("dd/MM/yyyy") + "' ORDER BY id DESC";


                return db.movimientos.SqlQuery(sql).Count();
            }
        }

        public List<Entities.movimientos> pagina_movi(int pageIndex, int pageSize, int id_usuario)
        {
            if (pageIndex > 0) { pageIndex = pageIndex - 1; }
            using (Dbtimetrakin db = new Dbtimetrakin())
            {
                string sql = @"SELECT * FROM traking.movimientos
                             where id_usuario="+id_usuario+" and date_format( fecha_registro_real,'%d/%m/%Y')='"+DateTime.Now.ToString("dd/MM/yyyy")+"' ORDER BY id DESC";

                return db.movimientos.SqlQuery(sql).ToList().Skip(pageSize * pageIndex).Take(pageSize).ToList();
            }
        }
    }
}
