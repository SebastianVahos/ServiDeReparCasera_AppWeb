﻿using Servicios.Clases;
using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Servicios.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Equipos")]
    [Authorize]

    public class EquiposController : ApiController
    {
        [HttpGet]
        [Route("ConsultarImagenes")]

        public IQueryable ConsultarImagenes(int CodigoEquipo)
        {
            clsEquipo Equipo = new clsEquipo();
            return Equipo.ListarImagenes(CodigoEquipo);
        }

        [HttpGet]
        [Route("ConsultarTodos")]
        public List<Equipo> ConsultarTodos()
        {
            clsEquipo Equipo = new clsEquipo();
            return Equipo.ConsultarTodos();
        }

        [HttpGet]
        [Route("Consultar")]
        public Equipo Consultar(int CodigoEquipo)
        {
            clsEquipo Equipo = new clsEquipo();
            return Equipo.Consultar(CodigoEquipo);
        }

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] Equipo equipo)
        {
            clsEquipo Equipo = new clsEquipo();
            Equipo.equipo = equipo;
            return Equipo.Insertar();
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] Equipo equipo)
        {
            clsEquipo Equipo = new clsEquipo();
            Equipo.equipo = equipo;
            return Equipo.Actualizar();
        }


        [HttpDelete]
        [Route("EliminarXCodigo")]
        public string EliminarXCodigo(int CodigoEquipo)
        {
            clsEquipo Equipo = new clsEquipo();
            return Equipo.Eliminar(CodigoEquipo);
        }

        [HttpGet]
        [Route("LlenarCombo")]
        public IQueryable LlenarCombo()
        {
            clsEquipo equipo = new clsEquipo();
            return equipo.LlenarCombo();
        }

        [HttpGet]
        [Route("LlenarComboImg")]
        public IQueryable LlenarComboImg(int codEquipo)
        {
            clsEquipo equipo = new clsEquipo();
            return equipo.LlenarComboImg(codEquipo);
        }

        [HttpDelete]
        [Route("EliminarEquipo")]
        public string Eliminar([FromBody] Equipo equipo)
        {
            clsEquipo equi = new clsEquipo();
            equi.equipo = equipo;
            return equi.Eliminar();
        }
    }
}