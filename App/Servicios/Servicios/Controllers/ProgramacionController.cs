﻿using Servicios.Clases;
using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Servicios.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/programaciones")]
    [Authorize]
    public class ProgramacionController : ApiController
    {

        ClaseProgramacion ClaseProgramacion = new ClaseProgramacion();

        [HttpGet]
        [Route("consultarTodos")]
        public List<Programacion> ObtenerServicios()
        {
            return ClaseProgramacion.ObtenerProgramaciones();
        }

        [HttpGet]
        [Route("consultar")]
        public Programacion ObtenerServicioPorId(int id)
        {
            return ClaseProgramacion.ObtenerProgramacionPorID(id);
        }

        [HttpPost]
        [Route("agregar")]
        public IHttpActionResult AgregarProgramacion([FromBody] Programacion programacion)
        {
            if (programacion == null)
            {
                return BadRequest("La programación no puede ser nula.");
            }

            ClaseProgramacion.AgregarProgramacion(programacion);
            return Ok("Programacion agregada     correctamente.");
        }

        [HttpPut]
        [Route("actualizarestado")]
        public IHttpActionResult ActualizarEstadoDeProgramacion(int programacion, string estado)
        {

            if (ClaseProgramacion.ObtenerProgramacionPorID(programacion) == null)
            {
                return BadRequest("No existe programación con ese ID");
            }

            ClaseProgramacion.ActualizarEstadoDeProgramacion(programacion, estado);
            return Ok("Se actualizó correctamente el estado de la programación.");
        }

        [HttpPut]
        [Route("actualizar")]
        public IHttpActionResult ActualizarProgramacion([FromBody] Programacion programacion)
        {
            if (programacion == null)
            {
                return BadRequest("La programación no puede ser nula.");
            }

            ClaseProgramacion.ActualizarProgramacion(programacion);
            return Ok("Programación actualizada correctamente.");
        }

        [HttpDelete]
        [Route("eliminar")]
        public IHttpActionResult EliminarProgramacion(int id)
        {
            ClaseProgramacion.EliminarProgramacion(id);
            return Ok("Programación eliminada correctamente.");
        }

    }
}