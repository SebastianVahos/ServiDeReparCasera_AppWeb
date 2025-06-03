using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Servicios.Clases
{
    public class clsFactura
    {
        private ServiDeReparCaserosEntities dbServiDeReparCaseros = new ServiDeReparCaserosEntities();

        public Factura factura { get; set; }
        public DetalleFactura detalleFactura { get; set; } // Corrige el nombre según tu modelo

        public string GrabarFactura()
        {
            if (factura.Numero == 0)
            {
                // Genera y guarda encabezado
                var numeroGuardado = GrabarEncabezado();
                if (!int.TryParse(numeroGuardado, out int nroFactura))
                {
                    return numeroGuardado; // Retorna el error
                }
                factura.Numero = nroFactura;
            }

            detalleFactura.NumeroFactura = factura.Numero; // Asegúrate que la FK se llame así en tu modelo

            return GrabarDetalle();
        }

        private string GrabarEncabezado()
        {
            try
            {
                factura.Numero = GenerarNumeroFactura();
                factura.Fecha = DateTime.Now;
                dbServiDeReparCaseros.Facturas.Add(factura);
                dbServiDeReparCaseros.SaveChanges();
                return factura.Numero.ToString();
            }
            catch (Exception ex)
            {
                return "Error al grabar encabezado: " + ex.Message;
            }
        }

        private int GenerarNumeroFactura()
        {
            return dbServiDeReparCaseros.Facturas.Select(f => (int?)f.Numero).DefaultIfEmpty(0).Max() + 1;
        }

        private string GrabarDetalle()
        {
            try
            {
                dbServiDeReparCaseros.DetalleFacturas.Add(detalleFactura);
                dbServiDeReparCaseros.SaveChanges();
                return factura.Numero.ToString();
            }
            catch (Exception ex)
            {
                return "Error al grabar detalle: " + ex.Message;
            }
        }

        public IQueryable ListarProductos(int numeroFactura)
        {
            var query = from d in dbServiDeReparCaseros.DetalleFacturas
                        join p in dbServiDeReparCaseros.Productos on d.CodigoProducto equals p.Codigo
                        join tp in dbServiDeReparCaseros.TipoProductos on p.CodigoTipoProducto equals tp.Codigo
                        where d.NumeroFactura == numeroFactura
                        select new
                        {
                            Eliminar = "<img src=\"../Imagenes/Eliminar.png\" onclick=\"Eliminar(" + d.Codigo + ", " + d.Cantidad + ", " + d.ValorUnitario + ")\"/>",
                            Tipo_Producto = tp.Nombre,
                            Producto = p.Nombre,
                            Cantidad = d.Cantidad,
                            Valor_Unitario = d.ValorUnitario,
                            Subtotal = d.Cantidad * d.ValorUnitario
                        };

            return query;
        }

        public string EliminarProducto(int codigo)
        {
            try
            {
                var detalle = dbServiDeReparCaseros.DetalleFacturas.FirstOrDefault(d => d.Codigo == codigo);
                if (detalle == null)
                    return "Producto no encontrado";

                dbServiDeReparCaseros.DetalleFacturas.Remove(detalle);
                dbServiDeReparCaseros.SaveChanges();
                return "Producto eliminado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al eliminar producto: " + ex.Message;
            }
        }


    }
}
