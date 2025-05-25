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
            _context.Programacions.Add(programacion);
            _context.SaveChanges();
        }

        public void ActualizarProgramacion(Programacion programacion)
        {
            var servicioExistente = _context.Programacions.FirstOrDefault(p => p.IdProgramacion == programacion.IdProgramacion);
            if (servicioExistente != null)
            {
                servicioExistente.Fecha = programacion.Fecha;
                servicioExistente.Hora = programacion.Hora;
                servicioExistente.Direccion = programacion.Direccion;
                servicioExistente.Estado = programacion.Estado;
                servicioExistente.Documento = programacion.Documento;
                servicioExistente.CodigoServicio = programacion.CodigoServicio;
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