using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Servicios.Clases
{
    public class clsFactura
    {
        private ServiDeReparCaserosEntities1 dbServiDeReparCaseros = new ServiDeReparCaserosEntities1();

        public Factura factura { get; set; }

        public string Insertar()
        {
            try
            {
                dbServiDeReparCaseros.Facturas.Add(factura);
                dbServiDeReparCaseros.SaveChanges();
                return "Factura insertada correctamente";
            }
            catch (Exception ex)
            {
                return "Error al insertar la factura: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                Factura fact = Consultar(factura.Numero);
                if (fact == null)
                {
                    return "La factura no existe, por lo tanto no se puede actualizar";
                }

                dbServiDeReparCaseros.Facturas.AddOrUpdate(factura);
                dbServiDeReparCaseros.SaveChanges();
                return "Factura actualizada correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo actualizar la factura: " + ex.Message;
            }
        }

        public List<Factura> ConsultarTodas()
        {
            return dbServiDeReparCaseros.Facturas
                .OrderBy(f => f.Fecha)
                .ToList();
        }

        public Factura Consultar(int Numero)
        {
            return dbServiDeReparCaseros.Facturas
                .FirstOrDefault(f => f.Numero == Numero);
        }

        public string Eliminar()
        {
            try
            {
                Factura fact = Consultar(factura.Numero);
                if (fact == null)
                {
                    return "La factura no existe, por lo tanto no se puede eliminar";
                }

                dbServiDeReparCaseros.Facturas.Remove(fact);
                dbServiDeReparCaseros.SaveChanges();
                return "Factura eliminada correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar la factura: " + ex.Message;
            }
        }

        public string EliminarXNumero(int Numero)
        {
            try
            {
                Factura fact = Consultar(Numero);
                if (fact == null)
                {
                    return "La factura no existe, por lo tanto no se puede eliminar";
                }

                dbServiDeReparCaseros.Facturas.Remove(fact);
                dbServiDeReparCaseros.SaveChanges();
                return "Factura eliminada correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar la factura: " + ex.Message;
            }
        }
    }
}
