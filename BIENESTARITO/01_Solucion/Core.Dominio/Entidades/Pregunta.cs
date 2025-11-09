using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Dominio.Entidades
{
    [Table("tbl_pregunta")]
    public class Pregunta
    {
        [Key]
        [Column("idpregunta")]
        public long IdPregunta { get; set; }

        [Column("idautoridad")]
        public long IdAutoridad { get; set; }

        [ForeignKey(nameof(IdAutoridad))]
        public required Autoridad Autoridad { get; set; }

        [Column("idtematica")]
        public long IdTematica { get; set; }

        [ForeignKey(nameof(IdTematica))]
        public required Tematica Tematica { get; set; }

        [Column("idsubtematica")]
        public long IdSubTematica { get; set; }

        [ForeignKey(nameof(IdSubTematica))]
        public required SubTematica SubTematica { get; set; }

        [Required]
        [Column("textoPregunta")]
        public required  string TextoPregunta { get; set; }

        [Column("activa")]
        public bool? Activa { get; set; }
        public required ICollection<Respuesta> Respuestas { get; set; }
    }
}
