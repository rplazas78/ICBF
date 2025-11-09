using System.ComponentModel.DataAnnotations;

namespace chatBoot.Database.Entidades
{
    public class Conversacion
    {
        [Key]
        public Guid IdConversacion { get; set; }
        public string? IdUsuarioDrupal { get; set; }
        public int? IdRegional { get; set; }
        public DateTime FechaCreacion { get; set; }

        public ICollection<Mensaje>? Mensajes { get; set; }
    }
}
