
using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Servicios.Clases
{
    public class clsEmpleado
    {
        // Contexto de la base de datos para operaciones EF
        private ServiDeReparCaserosEntities1 dbServiDeReparCaseros = new ServiDeReparCaserosEntities1();

        // Propiedad para manipular el empleado
        public Empleado empleado { get; set; }

        public string Insertar()
        {
            try
            {
                dbServiDeReparCaseros.Empleadoes.Add(empleado);
                dbServiDeReparCaseros.SaveChanges();
                return "Empleado insertado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al insertar el empleado: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                Empleado empl = Consultar(empleado.CCEmpleado);
                if (empl == null)
                {
                    return "El empleado con el documento ingresado no existe, por lo tanto no se puede actualizar";
                }

                dbServiDeReparCaseros.Empleadoes.AddOrUpdate(empleado);
                dbServiDeReparCaseros.SaveChanges();
                return "Se actualizó el empleado correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo actualizar el empleado: " + ex.Message;
            }
        }

        public List<Empleado> ConsultarTodos()
        {
            return dbServiDeReparCaseros.Empleadoes
                .OrderBy(e => e.NombreCompleto)
                .ToList();
        }

        public Empleado Consultar(string documento)
        {
            return dbServiDeReparCaseros.Empleadoes
                .FirstOrDefault(e => e.CCEmpleado == documento);
        }

        public string Eliminar()
        {
            try
            {
                Empleado empl = Consultar(empleado.CCEmpleado);
                if (empl == null)
                {
                    return "El empleado con el documento ingresado no existe, por lo tanto no se puede eliminar";
                }

                dbServiDeReparCaseros.Empleadoes.Remove(empl);
                dbServiDeReparCaseros.SaveChanges();
                return "Se eliminó el empleado correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el empleado: " + ex.Message;
            }
        }

        public string EliminarXDocumento(string documento)
        {
            try
            {
                Empleado empl = Consultar(documento);
                if (empl == null)
                {
                    return "El empleado con el documento ingresado no existe, por lo tanto no se puede eliminar";
                }

                dbServiDeReparCaseros.Empleadoes.Remove(empl);
                dbServiDeReparCaseros.SaveChanges();
                return "Se eliminó el empleado correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el empleado: " + ex.Message;
            }
        }
    }
}
