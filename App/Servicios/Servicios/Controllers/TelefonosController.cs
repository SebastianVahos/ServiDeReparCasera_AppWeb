using Servicios.Clases;
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
    [RoutePrefix("api/Telefonos")]
    [Authorize]
    public class TelefonosController : ApiController
    {
        [HttpGet]
        [Route("ListadoTelefonosXCliente")]
        public IQueryable ListadoTelefonosXCliente(string Documento)
        {
            clsTelefono telefono = new clsTelefono();
            return telefono.TelefonosXCliente(Documento);
        }
        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] Telefono telefono)
        {
            clsTelefono _telefono = new clsTelefono();
            _telefono.telefono = telefono;
            return _telefono.Insertar();
        }
        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] Telefono telefono)
        {
            clsTelefono _telefono = new clsTelefono();
            _telefono.telefono = telefono;
            return _telefono.Actualizar();
        }
        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar([FromBody] Telefono telefono)
        {
            clsTelefono _telefono = new clsTelefono();
            _telefono.telefono = telefono;
            return _telefono.Eliminar();
        }
    }
}