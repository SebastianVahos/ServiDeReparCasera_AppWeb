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
    [RoutePrefix("api/TipoContratos")]
    //[Authorize]
    public class TipoContratosController : ApiController
    {
        [HttpGet] //Es el servicio que se va a exponer: GET, POST, PUT, DELETE
        [Route("ConsultarTodos")] //Es el nombre de la funcionalidad que se va a ejecutar
        public List<TipoContrato> ConsultarTodos()
        {
            //Se crea una instancia de la clase clsTipoContrato
            clsTipoContrato TipoContrato = new clsTipoContrato();
            //Se invoca el método ConsultarTodos() de la clase clsTipoContrato
            return TipoContrato.ConsultarTodos();
        }
        [HttpGet]
        [Route("ConsultarXIdTipoContrato")]
        public TipoContrato ConsultarXIdTipoContrato(int IdTipoContrato)
        {
            //Se crea una instancia de la clases clsEmpleado
            clsTipoContrato TipoContrato = new clsTipoContrato();
            return TipoContrato.Consultar(IdTipoContrato);
        }

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] TipoContrato tipoContrato)
        {
            //Se crea una instancia de la clase clsEmpleado
            clsTipoContrato TipoContrato = new clsTipoContrato();
            //Se pasa la propieadad empleado al objeto de la clases clsEmpleado
            TipoContrato.tipoContrato = tipoContrato;
            //Se invoca el método insertar
            return TipoContrato.Insertar();
        }
        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] TipoContrato tipoContrato)
        {
            clsTipoContrato TipoContrato = new clsTipoContrato();
            TipoContrato.tipoContrato = tipoContrato;
            return TipoContrato.Actualizar();
        }
        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar([FromBody] TipoContrato tipoContrato)
        {
            clsTipoContrato TipoContrato = new clsTipoContrato();
            TipoContrato.tipoContrato = tipoContrato;
            return TipoContrato.Eliminar();
        }
        [HttpDelete]
        [Route("EliminarXIdTipoContrato")]
        public string EliminarXIdTipoContrato(int Documento)
        {
            clsTipoContrato TipoContrato = new clsTipoContrato();
            return TipoContrato.EliminarXIdTipoContrato(Documento);
        }
    }
}