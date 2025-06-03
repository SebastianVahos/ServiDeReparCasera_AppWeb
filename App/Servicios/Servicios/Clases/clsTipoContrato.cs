using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios.Clases
{
    public class clsTipoContrato
    {
        ServiDeReparCaserosEntities DBServi = new ServiDeReparCaserosEntities();
        public TipoContrato tipoContrato { get; set; }

        public List<TipoContrato> ConsultarTodos()
        {
            return DBServi.TipoContratoes
                .OrderBy(e => e.Nombre)
                .ToList();
        }

        public TipoContrato Consultar(int Documento)
        {
            return DBServi.TipoContratoes
                .FirstOrDefault(e => e.IdTipoContrato == Documento);
        }

        public string Insertar()
        {
            DBServi.TipoContratoes.Add(tipoContrato);
            DBServi.SaveChanges();
            return "Se insertó el Tipo de Contrato: " + tipoContrato.IdTipoContrato;
        }
        public string Eliminar()
        {
            TipoContrato _tipoContrato = DBServi.TipoContratoes.FirstOrDefault(t => t.IdTipoContrato == tipoContrato.IdTipoContrato);
            DBServi.TipoContratoes.Remove(_tipoContrato);
            DBServi.SaveChanges();
            return "Se eliminó el contrato con Id : " + tipoContrato.IdTipoContrato;
        }
        public string Actualizar()
        {
            TipoContrato _tipoContrato = DBServi.TipoContratoes.FirstOrDefault(t => t.IdTipoContrato == tipoContrato.IdTipoContrato);
            _tipoContrato.IdTipoContrato = tipoContrato.IdTipoContrato;
            _tipoContrato.Nombre = tipoContrato.Nombre;

            DBServi.SaveChanges();
            return "Se actualizó el Tipo Contrato con Id: " + tipoContrato.IdTipoContrato;
        }
        public string EliminarXIdTipoContrato(int documento)
        {
            try
            {
                TipoContrato empl = Consultar(documento);
                if (empl == null)
                {
                    return "El Tipo Contrato con el Id ingresado no existe, por lo tanto no se puede eliminar";
                }

                DBServi.TipoContratoes.Remove(empl);
                DBServi.SaveChanges();
                return "Se eliminó el Tipo Contrato correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el Tipo Contrato: " + ex.Message;
            }
        }
    }
}