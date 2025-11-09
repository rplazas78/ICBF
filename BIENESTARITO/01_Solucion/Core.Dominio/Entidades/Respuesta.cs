using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Dominio.Entidades
{
    public class Respuesta
    {
        [Key]
        [Column("idrespuesta")]
        public long IdRespuesta { get; set; }

        [Column("idpregunta")]
        public long IdPregunta { get; set; }
        public required Pregunta Pregunta { get; set; }

        [Column("idregional")]
        public long? IdRegional { get; set; }
        public Regional? Regional { get; set; }

        [Required]
        [Column("textorespuesta")]
        public required string TextoRespuesta { get; set; }

        [Required]
        [StringLength(500)]
        [Column("tiporespuesta")]
        public required string TipoRespuesta { get; set; }

        [StringLength(500)]
        [Column("urlenlace")]
        public string? UrlEnlace { get; set; } 
    }
}