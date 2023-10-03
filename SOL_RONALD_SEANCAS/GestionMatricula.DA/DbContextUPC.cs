using GestionMatricula.BE;
using GestionMatricula.UT;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionMatricula.DA
{
    public class DbContextUPC: DbContext
    {
        string defaultSchema = AppSettings.Get<string>("OracleDefaultSchema");

        public DbContextUPC() : base("DbContextUPC")
        {
        }

        public DbSet<AlumnoBE> Alumno { get; set; }
        public DbSet<CursoBE> Curso { get; set; }
        public DbSet<SeccionBE> Seccion { get; set; }
        public DbSet<SeccionCursoBE> SeccionCurso { get; set; }
        public DbSet<TipoMatriculaBE> TipoMatricula { get; set; }
        public DbSet<MatriculaBE> Matricula { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(defaultSchema);
        }
    }
}
