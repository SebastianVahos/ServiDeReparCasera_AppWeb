using Servicios.Clases;
using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Servicios.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Proveedores")]
    [Authorize]
    public class ProveedoresController : ApiController
    {
        [HttpGet]
        [Route("ConsultarTodos")]
        public List<Proveedor> ConsultarTodos()
        {
            clsProveedor proveedor = new clsProveedor();
            return proveedor.ConsultarTodos();
        }

        [HttpGet]
        [Route("Consultar")]
        public Proveedor Consultar(string idProveedor)
        {
            clsProveedor proveedor = new clsProveedor();
            return proveedor.Consultar(idProveedor);
        }

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] Proveedor proveedor, int codEquipo, decimal vlrUnitario, DateTime fechaCoti, DateTime fechaVali)
        {
            clsProveedor Proveedor = new clsProveedor();
            Proveedor.proveedor = proveedor;
            return Proveedor.Insertar(codEquipo, vlrUnitario, fechaCoti, fechaVali);
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] Proveedor proveedor)
        {
            clsProveedor Proveedor = new clsProveedor();
            Proveedor.proveedor = proveedor;
            return Proveedor.Actualizar();
        }

        [HttpDelete]
        [Route("EliminarXCodigo")]
        public string EliminarXCodigo(string idProveedor)
        {
            clsProveedor Proveedor = new clsProveedor();
            return Proveedor.Eliminar(idProveedor);
        }

    }
}