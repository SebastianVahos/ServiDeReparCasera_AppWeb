using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Servicios.Clases
{
    public class clsCliente
    {
        private ServiDeReparCaserosEntities DBServi = new ServiDeReparCaserosEntities();
        public Cliente cliente { get; set; }
        public string Insertar()
        {
            try
            {
                DBServi.Clientes.Add(cliente);
                DBServi.SaveChanges();
                return "Se creo el cliente correctamente";
            }
            catch (Exception ex)
            {
                return "Error al crear el cliente: " + ex.Message;
            }
        }
        public Cliente ConsultarXDocumento(string documento)
        {
            return DBServi.Clientes.FirstOrDefault(c => c.Documento == documento);
        }
        public List<Cliente> ConsultarTodos()
        {
            return DBServi.Clientes.ToList();
        }
        public string Actualizar()
        {
            try
            {
                Cliente clien = ConsultarXDocumento(cliente.Documento);
                if (clien == null)
                {
                    return "El cliente con el documento ingresado no existe, por lo tanto no se puede actualizar";
                }
                DBServi.Clientes.AddOrUpdate(cliente);
                DBServi.SaveChanges();
                return "Se actualizo el cliente correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo actualizar el empleado: " + ex.Message;
            }
        }
        public string Eliminar(string documento)
        {
            try
            {
                Cliente clien = ConsultarXDocumento(documento);
                if (clien == null)
                {
                    return "El cliente con el documento ingresado no existe, por lo tanto no se puede eliminar";
                }
                DBServi.Clientes.Remove(clien);
                DBServi.SaveChanges();
                return "Se elimino el cliente correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el cliente: " + ex.Message;
            }   
        }
    }
}