using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios.Models
{
    public class FacturaDetalle
    {
        public Factura factura { get; set; }
        public DetalleFactura detalle { get; set; }
    }
}