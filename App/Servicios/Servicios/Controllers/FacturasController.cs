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
    [RoutePrefix("api/Facturas")]
    [Authorize]
    public class FacturasController : ApiController
    {
        [HttpGet] //Es el servicio que se va a exponer: GET, POST, PUT, DELETE
        [Route("ConsultarTodos")] //Es el nombre de la funcionalidad que se va a ejecutar
        public List<Factura> ConsultarTodas()
        {
            //Se crea una instancia de la clase clsEmpleado
            clsFactura Factura = new clsFactura();
            //Se invoca el método ConsultarTodos() de la clase clsEmpleado
            return Factura.ConsultarTodas();
        }

        [HttpGet]
        [Route("ConsultarXNumero")]
        public Factura ConsultarXNumero(int Numero)
        {
            clsFactura factura = new clsFactura();
            return factura.Consultar(Numero);
        }

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] Factura facturaInput)
        {
            clsFactura factura = new clsFactura();
            factura.factura = facturaInput;
            return factura.Insertar();
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] Factura facturaInput)
        {
            clsFactura factura = new clsFactura();
            factura.factura = facturaInput;
            return factura.Actualizar();
        }

        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar([FromBody] Factura facturaInput)
        {
            clsFactura factura = new clsFactura();
            factura.factura = facturaInput;
            return factura.Eliminar();
        }

        [HttpDelete]
        [Route("EliminarXNumero")]
        public string EliminarXNumero(int Numero)
        {
            clsFactura factura = new clsFactura();
            return factura.EliminarXNumero(Numero);
        }
    }
}
