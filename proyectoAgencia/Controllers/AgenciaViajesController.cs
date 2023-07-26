using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using proyectoAgencia.Entities;
using proyectoAgencia.Interfaces;
using proyectoAgencia.Models;

namespace proyectoAgencia.Controllers
{
    public class AgenciaViajesController : Controller
    {

        private readonly IAgenciaViajesModel _agenciaViajesModel;

        public AgenciaViajesController(IAgenciaViajesModel agenciaViajesModel)
        {
            _agenciaViajesModel = agenciaViajesModel;
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarAgenciaViajes(AgenciaViajesEnt entidad)
        {
          
            var datos = _agenciaViajesModel.RegistrarAgenciaViajes(entidad);
            if (datos?.Codigo != 1)
            {
                ViewBag.Mensaje = datos?.Mensaje;
                return View("Registro");
            }

            return RedirectToAction("ConsultarAgenciaViajes", "AgenciaViajes");
        }

        [HttpGet]
        public IActionResult ConsultarAgenciaViajes()
        {
            var datos = _agenciaViajesModel.ConsultarAgenciaViajes();
            return View(datos?.Objetos);
        }

        [HttpGet]
        public IActionResult CambiarEstado(long q)
        {
            AgenciaViajesEnt entidad = new AgenciaViajesEnt();
            entidad.IdAgencia = q;

            var datos = _agenciaViajesModel.CambiarEstado(entidad);
            if (datos?.Codigo != 1)
            {
                ViewBag.Mensaje = datos?.Mensaje;
                return View("ConsultarAgenciaViajes");
            }

            return RedirectToAction("ConsultarAgenciaViajes", "AgenciaViajes");
        }

        [HttpGet]
        public IActionResult Editar(long q)
        {
            var datos = _agenciaViajesModel.ConsultarAgenciaViaje(q);
            return View(datos?.Objeto);
        }


    }
}
