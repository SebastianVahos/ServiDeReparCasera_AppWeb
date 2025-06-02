using Servicios.Clases;
using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Servicios.Controllers
{
    [RoutePrefix("api/servicios")]
    public class ServicioController : ApiController
    {
        ClaseServicio ClaseServicio = new ClaseServicio();

        [HttpGet]
        [Route("consultarTodos")]
        public List<Servicio> ObtenerServicios()
        {
            return ClaseServicio.ObtenerServicios();
        }

        [HttpGet]
        [Route("consultar")]
        public Servicio ObtenerServicioPorId(int id)
        {
            return ClaseServicio.ObtenerServicioPorId(id);
        }

        [HttpPost]
        [Route("agregar")]
        public IHttpActionResult AgregarServicio([FromBody] Servicio servicio)
        {
            if (servicio == null)
            {
                return BadRequest("El servicio no puede ser nulo.");
            }

            ClaseServicio.AgregarServicio(servicio);
            return Ok("Servicio agregado correctamente.");
        }

        [HttpPut]
        [Route("actualizar")]
        public IHttpActionResult ActualizarServicio([FromBody] Servicio servicio)
        {
            if (servicio == null)
            {
                return BadRequest("El servicio no puede ser nulo.");
            }

            ClaseServicio.ActualizarServicio(servicio);
            return Ok("Servicio actualizado correctamente.");
        }

        [HttpDelete]
        [Route("eliminar")]
        public IHttpActionResult EliminarServicio(int id)
        {   
            ClaseServicio.EliminarServicio(id);
            return Ok("Servicio eliminado correctamente.");
        }
    }
}