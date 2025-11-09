using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chatBoot.Database.Entidades
{
    [Table("tbl_autoridad")] 
    public class Autoridad
    {
        [Key]
        [Column("idautoridad")]
        public long IdAutoridad { get; set; } 

        [Required]
        [StringLength(500)]
        [Column("nombre")]
        public required string Nombre { get; set; }

        [Required]
        [Column("activa")]
        public bool Activa { get; set; }
        
        // Una Autoridad puede tener muchas Preguntas.
        public required ICollection<Pregunta> Preguntas { get; set; } = new List<Pregunta>();
    }
}