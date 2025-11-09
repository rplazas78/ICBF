using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dominio.Utils
{
    public class ServiceResponseData<T>
    {
        public bool State { get; set; }
        public int Code { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }

    public class CabeceraRespuesta
    {
        public string? Tipo { get; set; }
        public string? Titulo { get; set; }
        public string? Respuesta { get; set; }
        public string? Contexto { get; set; }
        public string? Flujo { get; set; }
        public string? TextoAnterior { get; set; }
        public string? ContextoAnterior { get; set; }
        public long? IdMenuAnterior { get; set; }
        public string? NivelArbol { get; set; }
    }
    public class DetalleRespuesta 
    {
        public long Id { get; set; }
        public string? Nombre { get; set; }
        public string? Pregunta { get; set; }
        public string? Respuesta { get; set; }
        public string? URL { get; set; }
    }
    public class MenuDetalleData 
    {
        public CabeceraRespuesta? CabeceraRespuesta { get; set; }
        public List<DetalleRespuesta>? ListaDetalleRespuesta { get; set; }
    }
}
