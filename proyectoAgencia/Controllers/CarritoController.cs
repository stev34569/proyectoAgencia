using proyectoAgencia.Entities;
using proyectoAgencia.Interfaces;
using proyectoAgencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace proyectoAgencia.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ICarritoModel _carritoModel;

        public CarritoController(ICarritoModel carritoModel)
        {
            _carritoModel = carritoModel;
        }

        [HttpGet]
        public IActionResult AgregarPaqueteCarrito(long q)
        {
            CarritoEnt entidad = new CarritoEnt();
            entidad.IdPaquete = q;

            _carritoModel.AgregarPaqueteCarrito(entidad);
            var datosCarrito = _carritoModel.ConsultarResumenCarrito();

            HttpContext.Session.SetString("Cantidad", datosCarrito?.Objeto.Cantidad.ToString());
            HttpContext.Session.SetString("SubTotal", datosCarrito?.Objeto.SubTotal.ToString());

            return RedirectToAction("PantallaPrincipal", "Home");
        }
    }
}
