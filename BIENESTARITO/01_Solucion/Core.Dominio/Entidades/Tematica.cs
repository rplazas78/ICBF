using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dominio.Entidades
{
    [Table("tbl_tematica")]
    public class Tematica
    {
        [Key]
        [Column("idtematica")]
        public long IdTematica { get; set; }

        [Required]
        [StringLength(500)]
        [Column("nombre")]
        public required string Nombre { get; set; } 

        [Required]
        [Column("activa")]
        public bool Activa { get; set; }
        
        // Una Tematica puede tener muchas Preguntas.
        public required ICollection<Pregunta> Preguntas { get; set; } = new List<Pregunta>();
    }
}
