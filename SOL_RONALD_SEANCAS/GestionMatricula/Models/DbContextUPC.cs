using GestionMatricula.BE;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GestionMatricula.Models
{
    public class DbContextUPC : DbContext
    {
        public DbContextUPC() : base("DbContextUPC")
        {
        }

        //public DbSet<AlumnoBE> Alumno { get; set; }
        public DbSet<CursoBE> Curso { get; set; }
        //public DbSet<SeccionBE> Seccion { get; set; }
        //public DbSet<SeccionCursoBE> SeccionCurso { get; set; }
        //public DbSet<MatriculaBE> Matricula { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Set a default schema for ALL tables
            modelBuilder.HasDefaultSchema("USRWEBCRMD");
        }
    }
}