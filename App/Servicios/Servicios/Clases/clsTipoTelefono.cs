using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios.Clases
{
    public class clsTipoTelefono
    {
        private ServiDeReparCaserosEntities DBServi = new ServiDeReparCaserosEntities();
        public IQueryable LlenarCombo()
        {
            return from T in DBServi.Set<TipoTelefono>()
                   orderby T.Nombre
                   select new
                   {
                       Codigo = T.Codigo,
                       Nombre = T.Nombre
                   };
        }
    }
}