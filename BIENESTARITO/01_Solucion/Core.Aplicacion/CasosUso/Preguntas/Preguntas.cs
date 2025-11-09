using Core.Dominio.Entidades;
using Core.Dominio.Utils;
using Infra.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.Aplicacion.CasosUso.Preguntas
{
    public class Preguntas : IPreguntas
    {
        private readonly ContextoBD _contexto;

        public Preguntas(ContextoBD contexto)
        {
            _contexto = contexto;
        }

        public ServiceResponseData<List<Pregunta>> Ejecutar(int pagina, int cantidadPorPagina)
        {
            //Inicializadores
            var result = new ServiceResponseData<List<Pregunta>>();
            result.Data = new List<Pregunta>();
 
            result.Code = 200;
            result.State = true;

            if (pagina < 1) pagina = 1;
            if (cantidadPorPagina < 1) cantidadPorPagina = 10; 

            int elementosASaltar = (pagina - 1) * cantidadPorPagina;

            var query = _contexto.Preguntas
                    .Skip(elementosASaltar)
                    .Take(cantidadPorPagina)
                    .Include(p => p.Autoridad)
                    .Include(p => p.Tematica)
                    .Include(p => p.SubTematica)
                    .ToList();

            result.Data = query;

            return result;
        }


        public ServiceResponseData<MenuDetalleData> Ejecutar_Accion(string contexto,string texto,string txtActual,string txtAnterior , string titulo, IEnumerable<string>? opciones, string flujo, string contextoAnterior, int nivelArbol, long idDato , string textoAnterior = "INICIO", long? idMenuAnterior = null)
        {
            //Inicializadores
            var result = new ServiceResponseData<MenuDetalleData>();
            CabeceraRespuesta cabeceraResp = new CabeceraRespuesta();
            List<DetalleRespuesta> query = new List<DetalleRespuesta>();

            result.Data = new MenuDetalleData();
            result.Code = 200;
            result.State = true;

            //MENÚ PRINCIPAL
            if (texto == "INICIO")
            {
                query.Add(new DetalleRespuesta
                {
                    Id = 1,
                    Nombre = "1. CONTINUAR - Continuar"
                });
            }


            //FLUJO: AUTORIDAD → TEMÁTICA
            if (contexto == "AUTORIDAD" && texto == "MENU_PRINCIPAL")
            {
                query = (from r in _contexto.Autoridades
                         where r.Activa == true 
                         select new DetalleRespuesta
                         {
                             Id = r.IdAutoridad,
                             Nombre = r.Nombre,
                         }).ToList();
            }


            //FLUJO: AUTORIDAD → TEMÁTICA
            if (contexto == "TEMATICA" && texto == "AUTORIDAD")
            {
                query = (from r in _contexto.Tematicas
                         where r.Activa == true && _contexto.Preguntas.Any(p => (idDato == -1 || p.IdAutoridad == idDato) && p.IdTematica == r.IdTematica)
                         select new DetalleRespuesta
                         {
                             Id = r.IdTematica,
                             Nombre = r.Nombre,
                         }).ToList();
            }


            //FLUJO: TEMÁTICA → SUBTEMÁTICA
            if (contexto == "SUBTEMATICA" && texto == "TEMATICA")
            {
                query = (from r in _contexto.SubTematicas
                         where r.Activa == true && _contexto.Preguntas.Any(p => (idDato == -1 || p.IdTematica == idDato) && p.IdSubTematica == r.IdSubTematica)
                         select new DetalleRespuesta
                         {
                             Id = r.IdSubTematica,
                             Nombre = r.Nombre,
                         }).ToList();
            }

            //FLUJO: SUBTEMÁTICA → PREGUNTAS
            if (contexto == "PREGUNTA" && texto == "SUBTEMATICA")
            {
                query = (from p in _contexto.Preguntas
                         where p.Activa == true && (idDato == -1 || p.IdSubTematica == idDato)
                         orderby p.IdPregunta
                         select new DetalleRespuesta
                         {
                             Id = p.IdPregunta,
                             Nombre = p.TextoPregunta,
                         }).ToList();
            }

            //FLUJO: RESPUESTA DE UNA PREGUNTA
            if (contexto == "RESPUESTA" && texto == "PREGUNTA")
            {
                var primerResultado = (from p in _contexto.Preguntas
                         join a in _contexto.Autoridades on p.IdAutoridad equals a.IdAutoridad
                         join t in _contexto.Tematicas on p.IdTematica equals t.IdTematica
                         join s in _contexto.SubTematicas on p.IdSubTematica equals s.IdSubTematica
                         join r in _contexto.Respuestas on p.IdPregunta equals r.IdPregunta into respGroup
                         from r in respGroup.DefaultIfEmpty()
                         where (idDato == -1 || p.IdPregunta == idDato)
                         select new DetalleRespuesta
                         {
                             Id = p.IdPregunta,
                             Nombre = "",
                             Pregunta = p.TextoPregunta,
                             Respuesta = r != null ? r.TextoRespuesta : "No hay respuesta registrada.",
                             URL = r != null ? r.UrlEnlace : null
                         }).FirstOrDefault();

                if (primerResultado != null)
                {
                    query.Add(primerResultado);
                }
                else
                {
                    query.Add(new DetalleRespuesta
                    {
                        Id = 0,
                        Nombre = "No encontré una respuesta para esa pregunta."
                    });
                }
            }


            //FLUJO: GENERAL
            if (contexto == "GENERAL")
            {
                var primerResultado = (from p in _contexto.Preguntas
                                       join a in _contexto.Autoridades on p.IdAutoridad equals a.IdAutoridad
                                       join t in _contexto.Tematicas on p.IdTematica equals t.IdTematica
                                       join s in _contexto.SubTematicas on p.IdSubTematica equals s.IdSubTematica
                                       join r in _contexto.Respuestas on p.IdPregunta equals r.IdPregunta into respGroup
                                       from r in respGroup.DefaultIfEmpty()
                                       where p.TextoPregunta.ToUpper().Contains(txtActual)
                                       select new DetalleRespuesta
                                       {
                                           Id = p.IdPregunta,
                                           Nombre = "",
                                           Pregunta = p.TextoPregunta,
                                           Respuesta = r != null ? r.TextoRespuesta : "No hay respuesta registrada.",
                                           URL = r != null ? r.UrlEnlace : null
                                       }).FirstOrDefault();

                if (primerResultado != null)
                {
                    query.Add(primerResultado);
                }
                else
                {
                    query.Add(new DetalleRespuesta
                    {
                        Id = 0,
                        Nombre = "No encontré una respuesta para esa pregunta."
                    });
                }
            }


            if (contexto != "MENU_PRINCIPAL")
            { 
                query.Add(new DetalleRespuesta
                {
                    Id = 99, 
                    Nombre = "VOLVER AL MENÚ ANTERIOR"
                });
            }

            query.Add(new DetalleRespuesta
            {
                Id = 100, 
                Nombre = "100. FINALIZAR CHAT"
            });

            result.Data.ListaDetalleRespuesta = query;

            cabeceraResp.Tipo = "menu";
            cabeceraResp.Titulo = titulo;
            cabeceraResp.Respuesta = "Escribe el número de una opción o INICIO para volver al menú.";
            cabeceraResp.Contexto = contexto;
            cabeceraResp.Flujo = flujo;
            cabeceraResp.TextoAnterior = textoAnterior;
            cabeceraResp.ContextoAnterior = contextoAnterior;
            cabeceraResp.IdMenuAnterior = idMenuAnterior;
            cabeceraResp.NivelArbol = nivelArbol.ToString();

            result.Data.CabeceraRespuesta = cabeceraResp;

            return result;

        }


        public ServiceResponseData<MenuDetalleData> VolverMenuAnterior(string contextoAnterior,string txtActual, string txtAnterior)
        {

            var result = new ServiceResponseData<MenuDetalleData>();

            CabeceraRespuesta cabeceraResp = new CabeceraRespuesta();
            List<DetalleRespuesta> query = new List<DetalleRespuesta>();

            result.Data = new MenuDetalleData();
            result.Code = 200;
            result.State = true;

            long.TryParse(txtActual, out long idDato);

            switch (contextoAnterior)
            {
                case "AUTORIDAD":
                    result = Ejecutar_Accion("AUTORIDAD", "MENU_PRINCIPAL", txtActual, txtAnterior, "Selecciona una autoridad:", null, "INICIO ← AUTORIDAD → TEMATICA", "MENU_PRINCIPAL", 2, idDato);
                    break;

                case "TEMATICA":
                    result = Ejecutar_Accion("TEMATICA", "AUTORIDAD", txtActual, txtAnterior, "Selecciona una autoridad:", null, "AUTORIDAD ← TEMATICA → SUBTEMATICA", "AUTORIDAD", 3, idDato);
                    break;

                case "SUBTEMATICA":
                    result = Ejecutar_Accion("SUBTEMATICA", "TEMATICA", txtActual, txtAnterior, "Selecciona una autoridad:", null, "AUTORIDAD ← TEMATICA → SUBTEMATICA", "TEMATICA", 4, idDato);
                    break;

                default:
                    result = Ejecutar_Accion("INICIO", "INICIO", txtActual, txtAnterior, "Selecciona una autoridad:", null, "NULL ← INICIO → AUTORIDAD", "", 1, idDato);
                    break;
            }

            return result;

        }


        public ServiceResponseData<MenuDetalleData> FinalizarChat(string contextoAnterior)
        {
            var result = new ServiceResponseData<MenuDetalleData>();

            CabeceraRespuesta cabeceraResp = new CabeceraRespuesta();
            List<DetalleRespuesta> query = new List<DetalleRespuesta>();

            result.Data = new MenuDetalleData();
            result.Code = 200;
            result.State = true;

            result.Data.ListaDetalleRespuesta = query;

            cabeceraResp.Tipo = "menu";
            cabeceraResp.Titulo = "Gracias por usar el ChatBoot del ICBF. ¡Hasta pronto!";
            cabeceraResp.Respuesta = "Gracias por usar el ChatBoot del ICBF. ¡Hasta pronto!";
            cabeceraResp.Contexto = "FINALIZADO";
            cabeceraResp.Flujo = "";
            cabeceraResp.TextoAnterior = "";
            cabeceraResp.ContextoAnterior = contextoAnterior;
            cabeceraResp.IdMenuAnterior = 0;
            cabeceraResp.NivelArbol = "X";

            result.Data.CabeceraRespuesta = cabeceraResp;

            return result;

        }
    }
}
