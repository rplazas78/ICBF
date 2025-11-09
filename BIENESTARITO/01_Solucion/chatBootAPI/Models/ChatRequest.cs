namespace chatBootAPI.Models
{
    public class ChatRequest
    {
        public string? Texto { get; set; } 
        public string? TextoAnterior { get; set; } 
        public string? IdUsuarioDrupal { get; set; }
        public int? IdRegional { get; set; }
        public string? Contexto { get; set; }
        public string? ContextoAnterior { get; set; }
    }
}
