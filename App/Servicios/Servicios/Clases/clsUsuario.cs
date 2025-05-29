using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios.Clases
{
    public class clsUsuario
    {
        private ServiDeReparCaserosEntities DBServi = new ServiDeReparCaserosEntities();
        public Usuario usuario { get; set; }
        public string CrearUsuario(int IdPerfil)
        {
            try
            {
                clsCypher cypher = new clsCypher();
                cypher.Password = usuario.Clave; // Esta clave es plana, osea 123*
                if (cypher.CifrarClave())
                {
                    //Grabar el usuario, se deben leer los datos de la clase cypher con la informacion encriptada
                    usuario.Clave = cypher.PasswordCifrado; //Aca la clave que estaba plana, ahora esta encriptada, la que guardo en la BD
                    usuario.Salt = cypher.Salt;
                    DBServi.Usuarios.Add(usuario);
                    DBServi.SaveChanges();
                    //Se debe grabar el perfil del usuario
                    PerfilUsuario UsuarioPerfil = new PerfilUsuario();
                    UsuarioPerfil.IdPerfil = IdPerfil;
                    UsuarioPerfil.Estado = true;
                    UsuarioPerfil.IdUsuario = usuario.IdUsuario; //El id del Usuario queda grabado en la clase usuario al grabar en la base de datos.
                    DBServi.PerfilUsuarios.Add(UsuarioPerfil);
                    DBServi.SaveChanges();
                    return "Se creo el usuario correctamente";
                }
                else
                {
                    return "No se pudo encriptar la clave del usuario";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}