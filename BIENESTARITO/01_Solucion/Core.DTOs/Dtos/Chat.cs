using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Dtos
{

    public class ChatRequestDTO
    {
        public string Texto { get; set; } = string.Empty;
        public string? IdUsuarioDrupal { get; set; }
        public int? IdRegional { get; set; }
        public string? Contexto { get; set; }
        public string? ContextoAnterior { get; set; }
    }

    public class ChatResponseDTO 
    { 
        public string prueba { get; set; } = string.Empty;
    }


}
