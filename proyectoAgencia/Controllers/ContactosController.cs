using Microsoft.AspNetCore.Mvc;
using proyectoAgencia.Entities;
using proyectoAgencia.Interfaces;
using proyectoAgencia.Models;


namespace proyectoAgencia.Controllers
{
    public class ContactosController : Controller
    {
        private readonly IContactosModel _contactosModel;
        private readonly IBitacoraModel _bitacoraModel;

        public ContactosController(IContactosModel contactosModel, IBitacoraModel bitacoraModel)
        {
            _contactosModel = contactosModel;
            _bitacoraModel = bitacoraModel;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Contacto()
        {
            return View();
        }


        [HttpPost]
        public IActionResult RegistrarContacto(ContactosEnt entidad)
        {
         
           
            var datos = _contactosModel.RegistrarContacto(entidad);
            if (datos?.Codigo != 1)
            {
                ViewBag.Mensaje = datos?.Mensaje;
                return View("Contacto");
            }

            return RedirectToAction("PantallaPrincipal", "Home");
        }

        [HttpGet]
        public IActionResult ConsultarContactos()
        {
            var datos = _contactosModel.ConsultarContactos();
            return View(datos?.Objetos);
        }

        [HttpGet]
        public IActionResult CambiarEstado(long q)
        {
            ContactosEnt entidad = new ContactosEnt();
            entidad.IdContacto = q;

            var datos = _contactosModel.CambiarEstado(entidad);
            if (datos?.Codigo != 1)
            {
                ViewBag.Mensaje = datos?.Mensaje;
                return View("ConsultarContactos");
            }

            return RedirectToAction("ConsultarContactos", "Contactos");
        }




    }
}



