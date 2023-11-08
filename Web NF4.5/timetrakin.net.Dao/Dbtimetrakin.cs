using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timetrakin.net.Entities;

namespace timetrakin.net.Dao
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class Dbtimetrakin: DbContext
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString;

        public Dbtimetrakin() : base(connectionString)
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<cat_estatus> cat_estatus { get; set; }
        public DbSet<Entities.movimientos> movimientos { get; set; }
        public DbSet<perfil> perfil { get; set; }
        public DbSet<proyecto> proyecto { get; set; }
        public DbSet<usuarios> usuarios { get; set; }
        public DbSet<muestra_movimientos> muestra_movimientos { get; set; }
        public DbSet<pais> pais { get; set; }
        public DbSet<cat_atencion> cat_atencion { get; set; }
        public DbSet<cat_torres> cat_torres { get; set; }
        public DbSet<vista_usuarios> vista_usuarios { get; set; }
        public DbSet<cat_actividades> cat_actividades { get; set; }
        public DbSet<token> token { get; set; }
    }
}
