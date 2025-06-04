using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Servicios.Clases
{
    public class clsProveedor
    {
        private ServiDeReparCaserosEntities DBServi = new ServiDeReparCaserosEntities();
        public Proveedor proveedor { get; set; }

        public string Insertar(int codEquipo, decimal vlrUnitario, DateTime fechaCoti, DateTime fechaVali)
        {
            try
            {
                DBServi.Proveedors.Add(proveedor);
                DBServi.SaveChanges();
                //Se debe grabar el equipo proveedor 
                EquipoProveedor equipoProveedor = new EquipoProveedor();
                equipoProveedor.IdProveedor = proveedor.IdProveedor;
                equipoProveedor.CodigoEquipo = codEquipo;
                equipoProveedor.ValorUnitario = vlrUnitario;
                equipoProveedor.FechaCotizacion = fechaCoti;
                equipoProveedor.FechaValidez = fechaVali;
                DBServi.EquipoProveedors.Add(equipoProveedor);
                DBServi.SaveChanges();
                return "Proveedor insertado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al insertar el proveedor: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                Proveedor prov = Consultar(proveedor.IdProveedor);
                if (prov == null)
                {
                    return "El proveedor con el código ingresado no existe, no se puede actualizar";
                }
                DBServi.Proveedors.AddOrUpdate(proveedor);
                DBServi.SaveChanges();
                return "Se actualizó el proveedor correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo actualizar el proveedor: " + ex.Message;
            }
        }

        public List<Proveedor> ConsultarTodos()
        {
            return DBServi.Proveedors.ToList();
        }
        public Proveedor Consultar(string idProveedor)
        {
            return DBServi.Proveedors.FirstOrDefault(e => e.IdProveedor == idProveedor);
        }
        public EquipoProveedor ConsultarDos(string idProveedor)
        {
            return DBServi.EquipoProveedors.FirstOrDefault(e => e.IdProveedor == idProveedor);
        }

        public string Eliminar(string idProveedor)
        {
            try
            {
                EquipoProveedor prov = ConsultarDos(idProveedor);
                if (prov == null)
                {
                    return "El proveedor con el codigo ingresado no existe, por lo tanto no se puede eliminar";
                }
                DBServi.EquipoProveedors.Remove(prov);
                DBServi.SaveChanges();
                Proveedor prove = Consultar(idProveedor);
                DBServi.Proveedors.Remove(prove);
                DBServi.SaveChanges();
                return "Se eliminó el proveedor correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el proveedor: " + ex.Message;
            }
        }

        public string Eliminar()
        {
            try
            {
                EquipoProveedor prov = DBServi.EquipoProveedors.FirstOrDefault(p => p.IdProveedor == proveedor.IdProveedor);
                DBServi.EquipoProveedors.Remove(prov);
                DBServi.SaveChanges();
                Proveedor prove = DBServi.Proveedors.FirstOrDefault(e => e.IdProveedor == proveedor.IdProveedor);
                DBServi.Proveedors.Remove(prove);
                DBServi.SaveChanges();
                return "Se eliminó el proveedor con el id: " + proveedor.IdProveedor;
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el proveedor: " + ex.Message;
            }
        }
    }
}