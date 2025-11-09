using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Dominio.Entidades
{
    public class Mensaje
    {
        [Key]
        public long IdMensaje { get; set; }
        public Guid IdConversacion { get; set; }
        public string? Rol { get; set; }   
        public string? Texto { get; set; }
        public DateTime FechaCreacion { get; set; }

        [ForeignKey("IdConversacion")]     
        public Conversacion? Conversacion { get; set; }
    }
}
