using chatBootAPI.Models;
using Core.Aplicacion.CasosUso.Preguntas;
using Core.Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace chatBotAPI.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChatBootController : ControllerBase
    {

        private readonly IPreguntas _preguntas;

        public ChatBootController(IPreguntas preguntas)
        {
            _preguntas = preguntas;
        }


        [HttpGet("preguntas")]
        public IActionResult GetPreguntas([FromQuery] int pagina = 1, [FromQuery] int cantidad = 10)
        {
            var preguntas = _preguntas.Ejecutar(pagina, cantidad);
            return Ok(preguntas);
        }

        [HttpPost("preguntar")]
        public IActionResult Preguntar([FromBody] ChatRequest request)
        {

            //INICIO → AUTORIDAD → TEMÁTICA → SUBTEMÁTICA → PREGUNTA → RESPUESTA → FINALIZAR

            string texto = request.Texto?.Trim().ToUpper() ?? "";
            string textoAnterior = request.TextoAnterior?.Trim().ToUpper() ?? "";
            string contexto = request.Contexto?.Trim().ToUpper() ?? "";
            string contextoAnterior = request.ContextoAnterior?.ToUpper() ?? "MENU_PRINCIPAL";


            //OPCIÓN VOLVER
            if (textoAnterior == "99" || textoAnterior == "VOLVER")
            {
                var preguntas = _preguntas.VolverMenuAnterior(contextoAnterior, texto, textoAnterior);
                return Ok(preguntas);
            }

            //OPCIÓN FINALIZAR
            if (texto == "100" || textoAnterior == "100" || texto == "FINALIZAR")
            {
                var preguntas = _preguntas.FinalizarChat(contextoAnterior);
                return Ok(preguntas);
            }

            //MENÚ PRINCIPAL
            if (texto == "INICIO" || texto == "MENU" || texto == "HOLA")
            {
                var preguntas = _preguntas.Ejecutar_Accion("MENU_PRINCIPAL","INICIO", texto, textoAnterior, "Bienvenido al ChatBot del ICBF", null, "NULL ← INICIO → AUTORIDAD", "INICIO", 1,0);
                return Ok(preguntas);
            }

            //SELECCIÓN DEL MENÚ PRINCIPAL
            if (contexto == "MENU_PRINCIPAL" && long.TryParse(texto, out long opcion))
            {
                if (opcion == 1)
                {
                    var preguntas = _preguntas.Ejecutar_Accion("AUTORIDAD", "MENU_PRINCIPAL", texto, textoAnterior, "Selecciona una autoridad:", null, "INICIO ← AUTORIDAD → TEMATICA", "MENU_PRINCIPAL", 2, opcion);
                    return Ok(preguntas);
                }
            }

            //FLUJO: AUTORIDAD → TEMÁTICA
            if (contexto == "AUTORIDAD" && long.TryParse(texto, out long idAutoridad))
            {
                var preguntas = _preguntas.Ejecutar_Accion("TEMATICA", "AUTORIDAD", texto, textoAnterior, "Selecciona una autoridad:", null, "AUTORIDAD ← TEMATICA → SUBTEMATICA", "AUTORIDAD", 3, idAutoridad);
                return Ok(preguntas);
            }

            //FLUJO: TEMÁTICA → SUBTEMÁTICA
            if (contexto == "TEMATICA" && long.TryParse(texto, out long idTematica))
            {
                var preguntas = _preguntas.Ejecutar_Accion("SUBTEMATICA", "TEMATICA", texto, textoAnterior, "Selecciona una temática:", null, "AUTORIDAD ← TEMATICA → SUBTEMATICA", "AUTORIDAD", 4, idTematica);
                return Ok(preguntas);
            }

            //FLUJO: SUBTEMÁTICA → PREGUNTAS
            if (contexto == "SUBTEMATICA" && long.TryParse(texto, out long idSubtematica))
            {
                var preguntas = _preguntas.Ejecutar_Accion("PREGUNTA", "SUBTEMATICA", texto, textoAnterior, "Selecciona una subtemática:", null, "TEMATICA ← SUBTEMATICA → PREGUNTAS", "SUBTEMATICA", 5, idSubtematica);
                return Ok(preguntas);
            }

            //FLUJO: SUBTEMÁTICA → PREGUNTAS
            if (contexto == "PREGUNTA" && long.TryParse(texto, out long idPregunta))
            {
                var preguntas = _preguntas.Ejecutar_Accion("RESPUESTA", "PREGUNTA", texto, textoAnterior, "Selecciona una pregunta:", null, "TEMATICA ← SUBTEMATICA → PREGUNTAS", "SUBTEMATICA", 6, idPregunta);
                return Ok(preguntas);
            }


            //FLUJO: GENERAL
            if (contexto == "GENERAL")
            {
                var preguntas = _preguntas.Ejecutar_Accion("GENERAL", "GENERAL", texto, textoAnterior, "Selecciona una pregunta:", null, "TEMATICA ← SUBTEMATICA → PREGUNTAS", "SUBTEMATICA", 7, -1);
                return Ok(preguntas);
            }


            //SI NO ENCUENTRA NADA
            return Ok(new
            {
                tipo = "texto",
                respuesta = "No encontré información relacionada. Escribe INICIO para ver las opciones disponibles.",
                contexto = "MENU_PRINCIPAL"
            });
        }
    }
}
