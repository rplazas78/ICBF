using System.ComponentModel.DataAnnotations;

namespace Core.Dominio.Entidades
{
    public class Regional
    {
        [Key]
        public long IdRegional { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }

        public ICollection<Respuesta>? Respuestas { get; set; }
    }
}