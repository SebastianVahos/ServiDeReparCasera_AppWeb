using Servicios.Clases;
using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servicios.Controllers
{
    [RoutePrefix("api/Equipos")]
    [Authorize]

    public class EquiposController : ApiController
    {
        [HttpGet]
        [Route("ConsultarImagenes")]

        public IQueryable ConsultarImagenes(int CodigoEquipo)
        {
            clsEquipo Equipo = new clsEquipo();
            return Equipo.ListarImagenes(CodigoEquipo);
        }

        [HttpGet]
        [Route("ConsultarTodos")]
        public List<Equipo> ConsultarTodos()
        {
            clsEquipo Equipo = new clsEquipo();
            return Equipo.ConsultarTodos();
        }

        [HttpGet]
        [Route("Consultar")]
        public Equipo Consultar(int CodigoEquipo)
        {
            clsEquipo Equipo = new clsEquipo();
            return Equipo.Consultar(CodigoEquipo);
        }

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] Equipo equipo)
        {
            clsEquipo Equipo = new clsEquipo();
            Equipo.equipo = equipo;
            return Equipo.Insertar();
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] Equipo equipo)
        {
            clsEquipo Equipo = new clsEquipo();
            Equipo.equipo = equipo;
            return Equipo.Actualizar();
        }


        [HttpDelete]
        [Route("EliminarXCodigo")]
        public string EliminarXCodigo(int CodigoEquipo)
        {
            clsEquipo Equipo = new clsEquipo();
            return Equipo.Eliminar(CodigoEquipo);
        }
    }
}