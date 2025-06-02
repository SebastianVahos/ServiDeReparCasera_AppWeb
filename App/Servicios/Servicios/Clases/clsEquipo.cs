using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Servicios.Clases
{
    public class clsEquipo
    {
        private ServiDeReparCaserosEntities DBServi = new ServiDeReparCaserosEntities();
        public Equipo equipo { get; set; }

        public string Insertar()
        {
            try
            {
                DBServi.Equipoes.Add(equipo);
                DBServi.SaveChanges();
                return "Equipo insertado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al insertar el equipo: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                Equipo equi = Consultar(equipo.CodigoEquipo);
                if (equi == null)
                {
                    return "El equipo con el código ingresado no existe, no se puede actualizar";
                }
                DBServi.Equipoes.AddOrUpdate(equipo);
                DBServi.SaveChanges();
                return "Se actualizó el equipo correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo actualizar el equipo: " + ex.Message;
            }
        }

        public List<Equipo> ConsultarTodos()
        {
            return DBServi.Equipoes.ToList();
        }
        public Equipo Consultar(int CodigoEquipo)
        {
            return DBServi.Equipoes.FirstOrDefault(e => e.CodigoEquipo == CodigoEquipo);
        }
        public string Eliminar(int CodigoEquipo)
        {
            try
            {
                Equipo equi = Consultar(CodigoEquipo);
                if (equi == null)
                {
                    return "El equipo con el codigo ingresado no existe, por lo tanto no se puede eliminar";
                }
                DBServi.Equipoes.Remove(equi);
                DBServi.SaveChanges();
                return "Se eliminó el equipo correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el equipo: " + ex.Message;
            }
        }
        public string GrabarImagenEquipo(int CodigoEquipo, List<string> Imagenes)
        {
            try
            {
                foreach (string imagen in Imagenes)
                {
                    ImagenesEquipo imagenEquipo = new ImagenesEquipo();
                    imagenEquipo.CodigoEquipo = CodigoEquipo;
                    imagenEquipo.NombreImagen = imagen;
                    DBServi.ImagenesEquipoes.Add(imagenEquipo);
                    DBServi.SaveChanges();
                }
                return "Se grabó la información en la base de datos";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        public IQueryable ListarImagenes(int CodigoEquipo)
        {
            return from E in DBServi.Set<Equipo>()
                   join I in DBServi.Set<ImagenesEquipo>()
                   on E.CodigoEquipo equals I.CodigoEquipo
                   where E.CodigoEquipo == CodigoEquipo
                   select new
                   {
                       codEquipo = E.CodigoEquipo,
                       Equipo = E.Nombre,
                       Imagen = I.NombreImagen
                   };
        }

        public string EliminarImagenEquipo(int idImagen)
        {
            try
            {
                var IdImagen = DBServi.ImagenesEquipoes.Where(c => c.CodigoEquipo == idImagen).FirstOrDefault();
                DBServi.ImagenesEquipoes.Remove(IdImagen);
                DBServi.SaveChanges();
                return "Se elimino la informacion en la base de datos";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}