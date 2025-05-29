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
    [RoutePrefix("api/Usuarios")]
    public class UsuariosController : ApiController
    {
        [HttpPost]
        [Route("CrearUsuario")]
        public string CrearUsuario([FromBody] Usuario usuario, int idPerfil)
        {
            clsUsuario Usuario = new clsUsuario();
            Usuario.usuario = usuario;
            return Usuario.CrearUsuario(idPerfil);
        }
    }
}