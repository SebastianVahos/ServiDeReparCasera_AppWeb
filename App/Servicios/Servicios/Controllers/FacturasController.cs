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
    using System.Web.Http;
    using System.Web.Http.Cors;

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Facturas")]
    [Authorize]
    public class FacturasController : ApiController
    {
        private readonly clsFactura _facturaService = new clsFactura();

        // GET api/Facturas/ListarProductos?NumeroFactura=123
        [HttpGet]
        [Route("ListarProductos")]
        public IHttpActionResult ListarProductos(int NumeroFactura)
        {
            if (NumeroFactura <= 0)
                return BadRequest("Número de factura inválido.");

            var productos = _facturaService.ListarProductos(NumeroFactura);
            if (productos == null)
                return NotFound();

            return Ok(productos);
        }

        // POST api/Facturas/GrabarFactura
        [HttpPost]
        [Route("GrabarFactura")]
        public IHttpActionResult GrabarFactura([FromBody] FacturaDetalle facturaDet)
        {
            if (facturaDet == null || facturaDet.factura == null || facturaDet.detalle == null)
                return BadRequest("Datos de factura inválidos.");

            _facturaService.factura = facturaDet.factura;
            _facturaService.detalleFactura = facturaDet.detalle;

            var resultado = _facturaService.GrabarFactura();

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok(resultado);
        }

        // DELETE api/Facturas/Eliminar?NumeroDetalle=123
        [HttpDelete]
        [Route("Eliminar")]
        public IHttpActionResult Eliminar(int NumeroDetalle)
        {
            if (NumeroDetalle <= 0)
                return BadRequest("Número de detalle inválido.");

            var resultado = _facturaService.EliminarProducto(NumeroDetalle);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok(resultado);
        }
    }

}
