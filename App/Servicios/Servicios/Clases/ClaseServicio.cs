using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios.Clases
{
    public class ClaseServicio
    {
        private readonly ServiDeReparCaserosEntities _context;

        public ClaseServicio()
        {
            _context = new ServiDeReparCaserosEntities();
        }

        public List<Servicio> ObtenerServicios()
        {
            return _context.Servicios.ToList();
        }

        public Servicio ObtenerServicioPorId(int id)
        {
            return _context.Servicios.FirstOrDefault(s => s.CodigoServicio == id);
        }

        public void AgregarServicio(Servicio servicio)
        {
            _context.Servicios.Add(servicio);
            _context.SaveChanges();
        }

        public void ActualizarServicio(Servicio servicio)
        {
            var servicioExistente = _context.Servicios.FirstOrDefault(s => s.CodigoServicio == servicio.CodigoServicio);
            if (servicioExistente != null)
            {
                servicioExistente.Nombre = servicio.Nombre;
                servicioExistente.Descripcion = servicio.Descripcion;
                servicioExistente.Precio = servicio.Precio;
                _context.SaveChanges();
            }
        }
        public void EliminarServicio(int id)
        {
            var servicio = _context.Servicios.FirstOrDefault(s => s.CodigoServicio == id);
            if (servicio != null)
            {
                _context.Servicios.Remove(servicio);
                _context.SaveChanges();
            }
        }
    }
}