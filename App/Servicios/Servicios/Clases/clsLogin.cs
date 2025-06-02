using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios.Clases
{
    public class clsLogin
    {
        public clsLogin()
        {
            loginRespuesta = new LoginRespuesta();//Cuando me construyan el objeto, creo la respuesta para poderla utilizar
        }
        private ServiDeReparCaserosEntities DBServi = new ServiDeReparCaserosEntities();
        public Login login { get; set; }
        public LoginRespuesta loginRespuesta { get; set; }
        public bool ValidarUsuario()
        {
            try
            {
                clsCypher cifrar = new clsCypher();//Creamos un objeto cifrar, para poder cifrar la clave
                Usuario usuario = DBServi.Usuarios.FirstOrDefault(u => u.UserName == login.Usuario);//lea el usuario
                if (usuario == null)
                {
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "Usuario no existe";
                    return false;
                }
                byte[] arrBytesSalt = Convert.FromBase64String(usuario.Salt);//obtiene el salt y lo convierte a Bytes
                string ClaveCifrada = cifrar.HashPassword(login.Clave, arrBytesSalt);//tiene una clave plana(login.clave) y encripta la clave con el arreglo de bytes que tiene
                login.Clave = ClaveCifrada;//y lo asigna a esa clave y ya esa clave esta encriptada
                return true;
            }
            catch (Exception ex)
            {
                loginRespuesta.Autenticado = false;
                loginRespuesta.Mensaje = ex.Message;
                return false;
            }
        }
        private bool ValidarClave()
        {
            try
            {                                                                                       //ya esta clave esta encriptada(u.clave) y compara la encripcion con encripcion que esta en la BD
                Usuario usuario = DBServi.Usuarios.FirstOrDefault(u => u.UserName == login.Usuario && u.Clave == login.Clave);
                if (usuario == null)
                {
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "La clave no coincide";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                loginRespuesta.Autenticado = false;
                loginRespuesta.Mensaje = ex.Message;
                return false;
            }
        }
        public IQueryable<LoginRespuesta> Ingresar()
        {
            if (ValidarUsuario() && ValidarClave())
            {
                //Se genera el token
                string Token = TokenGenerator.GenerateTokenJwt(login.Usuario);
                return from U in DBServi.Set<Usuario>()
                       join UP in DBServi.Set<PerfilUsuario>()
                       on U.IdUsuario equals UP.IdUsuario
                       join P in DBServi.Set<Perfil>()
                       on UP.IdPerfil equals P.IdPerfil
                       where U.UserName == login.Usuario &&
                               U.Clave == login.Clave
                       select new LoginRespuesta
                       {
                           Usuario = U.UserName,
                           Autenticado = true,
                           Perfil = P.Nombre,
                           PaginaInicio = P.PaginaNavegar,
                           Token = Token,
                           Mensaje = ""
                       };
            }
            else
            {
                List<LoginRespuesta> listRpta = new List<LoginRespuesta>();
                listRpta.Add(loginRespuesta);
                return listRpta.AsQueryable();
            }
        }
    }
}