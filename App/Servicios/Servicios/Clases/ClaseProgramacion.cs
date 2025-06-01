using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios.Clases
{
    public class ClaseProgramacion
    {

        private readonly ServiDeReparCaserosEntities _context;

        public ClaseProgramacion()
        {
            _context = new ServiDeReparCaserosEntities();
        }

        public List<Programacion> ObtenerProgramaciones()
        {
            return _context.Programacions.ToList();
        }

        public Programacion ObtenerProgramacionPorID(int id)
        {
            return _context.Programacions.FirstOrDefault(p => p.IdProgramacion == id);
        }

        public void AgregarProgramacion(Programacion programacion)
        {
            if(programacion.Estado == null || programacion.Estado == "")
            {
                programacion.Estado = "registrada";
            }
            _context.Programacions.Add(programacion);
            _context.SaveChanges();
        }

        public void ActualizarProgramacion(Programacion programacion)
        {
            var ProgramacionExistente = _context.Programacions.FirstOrDefault(p => p.IdProgramacion == programacion.IdProgramacion);
            if (ProgramacionExistente != null)
            {
                ProgramacionExistente.Fecha = programacion.Fecha;
                ProgramacionExistente.Hora = programacion.Hora;
                ProgramacionExistente.Direccion = programacion.Direccion;
                ProgramacionExistente.Estado = programacion.Estado;
                ProgramacionExistente.Documento = programacion.Documento;
                ProgramacionExistente.CodigoServicio = programacion.CodigoServicio;
                _context.SaveChanges();
            }
        }

        public void ActualizarEstadoDeProgramacion(int programacion, string Estado)
        {
            var ProgramacionExistente = _context.Programacions.FirstOrDefault(p => p.IdProgramacion == programacion);
            if (ProgramacionExistente != null)
            {
                ProgramacionExistente.Estado = Estado;
                _context.SaveChanges();
            }
        }
        public void EliminarProgramacion(int id)
        {
            var servicio = _context.Programacions.FirstOrDefault(p => p.IdProgramacion == id);
            if (servicio != null)
            {
                _context.Programacions.Remove(servicio);
                _context.SaveChanges();
            }
        }

    }
}