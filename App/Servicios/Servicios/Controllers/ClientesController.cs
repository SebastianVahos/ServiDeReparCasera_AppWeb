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
    [RoutePrefix("api/Clientes")]
    [Authorize]
    public class ClientesController : ApiController
    {
        [HttpGet]
        [Route("ConsultarTodos")]
        public List<Cliente> ConsultarTodos()
        {
            clsCliente cliente = new clsCliente();
            return cliente.ConsultarTodos();
        }
        [HttpGet]
        [Route("ConsultarXDocumento")]
        public Cliente ConsultarXDocumento(string documento)
        {
            clsCliente cliente = new clsCliente();
            return cliente.ConsultarXDocumento(documento);
        }
        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] Cliente cliente)
        {
            clsCliente Cliente = new clsCliente();
            Cliente.cliente = cliente;
            return Cliente.Insertar();
        }
        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] Cliente cliente)
        {
            clsCliente Cliente = new clsCliente();
            Cliente.cliente = cliente;
            return Cliente.Actualizar();
        }
        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar(string documento)
        {
            clsCliente cliente = new clsCliente();
            return cliente.Eliminar(documento);
        }
        [HttpDelete]
        [Route("EliminarCliente")]
        public string Eliminar([FromBody] Cliente cliente)
        {
            clsCliente _cliente = new clsCliente();
            _cliente.cliente = cliente;
            return _cliente.Eliminar();
        }
        [HttpGet]
        [Route("ClientesConTelefonos")]
        public IQueryable ClientesConTelefonos()
        {
            //Se crea el objeto de la clase ClsCliente, y se invoca el método Consultar
            clsCliente _ciente = new clsCliente();
            return _ciente.ConsultarConTelefono();
        }
    }
}