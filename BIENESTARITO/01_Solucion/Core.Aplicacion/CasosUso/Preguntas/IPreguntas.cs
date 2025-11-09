using Core.Dominio.Entidades;
using Core.Dominio.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aplicacion.CasosUso.Preguntas
{
    public interface IPreguntas
    {
        public ServiceResponseData<List<Pregunta>> Ejecutar(int pagina, int cantidadPorPagina);
        public ServiceResponseData<MenuDetalleData> Ejecutar_Accion(string contexto, string texto, string txtActual, string txtAnterior, string titulo, IEnumerable<string>? opciones, string flujo, string contextoAnterior, int nivelArbol, long idDato, string textoAnterior = "INICIO", long? idMenuAnterior = null);
        public ServiceResponseData<MenuDetalleData> VolverMenuAnterior(string contextoAnterior, string txtActual, string txtAnterior);
    }
}
