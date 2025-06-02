using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios.Clases
{
    public class clsTelefono
    {
        ServiDeReparCaserosEntities DBServi = new ServiDeReparCaserosEntities();
        public Telefono telefono { get; set; }
        public IQueryable TelefonosXCliente(string Documento)
        {
            return from C in DBServi.Set<Cliente>()
                   join T in DBServi.Set<Telefono>()
                   on C.Documento equals T.Documento
                   join TT in DBServi.Set<TipoTelefono>()
                   on T.CodigoTipoTelefono equals TT.Codigo
                   where C.Documento == Documento
                   orderby TT.Nombre
                   select new
                   {
                       Edit = "<img src=\"../Imagenes/Editar.png\" onclick=\"EditarTelefono(" + T.Codigo + ", " + TT.Codigo + ", " + T.Numero + ") \"style=\"cursor:grab\"/>",
                       Tipo_Telefono = TT.Nombre,
                       Numero = T.Numero
                   };
        }
        public string Insertar()
        {
            DBServi.Telefonoes.Add(telefono);
            DBServi.SaveChanges();
            return "Se insertó el teléfono: " + telefono.Numero;
        }
        public string Eliminar()
        {
            Telefono _telefono = DBServi.Telefonoes.FirstOrDefault(t => t.Codigo == telefono.Codigo);
            DBServi.Telefonoes.Remove(_telefono);
            DBServi.SaveChanges();
            return "Se eliminó el teléfono con código: " + telefono.Codigo;
        }
        public string Actualizar()
        {
            Telefono _telefono = DBServi.Telefonoes.FirstOrDefault(t => t.Codigo == telefono.Codigo);
            _telefono.CodigoTipoTelefono = telefono.CodigoTipoTelefono;
            _telefono.Numero = telefono.Numero;

            DBServi.SaveChanges();
            return "Se actualizó el teléfono con código: " + telefono.Codigo;
        }
    }
}