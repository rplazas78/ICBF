using Core.Dominio.Entidades;
using Infra.Persistencia.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infra.Persistencia.Contexto 
{
    public class ContextoBD : DbContext
    {
        private readonly DBOptions _DBOptions;

        public ContextoBD (IOptions<DBOptions> DBOptions, DbContextOptions<ContextoBD> contextOptions): base (contextOptions) 
        {
            _DBOptions = DBOptions.Value;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string cadenaConexion = _DBOptions.cadenaConexion;
                optionsBuilder.UseSqlServer(cadenaConexion);
            }
        }

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
