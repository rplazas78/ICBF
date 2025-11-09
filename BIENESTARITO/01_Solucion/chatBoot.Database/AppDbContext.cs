using Microsoft.EntityFrameworkCore;
using chatBoot.Database.Entidades;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace chatBoot.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Definición de tablas
        public DbSet<Autoridad> Autoridades { get; set; }
        public DbSet<Tematica> Tematicas { get; set; }
        public DbSet<SubTematica> SubTematicas { get; set; }
        public DbSet<Pregunta> Preguntas { get; set; }
        public DbSet<Respuesta> Respuestas { get; set; }
        public DbSet<Regional> Regionales { get; set; }
        public DbSet<Conversacion> Conversaciones { get; set; }
        public DbSet<Mensaje> Mensajes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración básica
            modelBuilder.Entity<Autoridad>().ToTable("tbl_autoridad");
            modelBuilder.Entity<Tematica>().ToTable("tbl_tematica");
            modelBuilder.Entity<SubTematica>().ToTable("tbl_subtematica");
            modelBuilder.Entity<Pregunta>().ToTable("tbl_pregunta");
            modelBuilder.Entity<Respuesta>().ToTable("tbl_respuesta");
            modelBuilder.Entity<Regional>().ToTable("tbl_regional");
            modelBuilder.Entity<Conversacion>().ToTable("tbl_conversacion");
            modelBuilder.Entity<Mensaje>().ToTable("tbl_mensaje");

            base.OnModelCreating(modelBuilder);
        }
    }
}
