using Servicios.Clases;
using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Servicios.Controllers
{
    [RoutePrefix("api/programaciones")]
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
            return Ok("Programacion agregada correctamente.");
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