//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Servicios.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class EquipoProveedor
    {
        public int Codigo { get; set; }
        public string IdProveedor { get; set; }
        public int CodigoEquipo { get; set; }
        public decimal ValorUnitario { get; set; }
        public System.DateTime FechaCotizacion { get; set; }
        public System.DateTime FechaValidez { get; set; }
        [JsonIgnore]
        public virtual Equipo Equipo { get; set; }
        [JsonIgnore]
        public virtual Proveedor Proveedor { get; set; }
    }
}
