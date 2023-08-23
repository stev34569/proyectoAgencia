using proyectoAgencia.Entities;
using proyectoAgencia.Interfaces;
using proyectoAgencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace proyectoAgencia.Controllers
{
    [FiltroSesion]
    [ResponseCache(NoStore = true, Duration = 0)]
    public class CarritoController : Controller
    {
        private readonly ICarritoModel _carritoModel;
        private readonly IPaquetesModel _paquetesModel;

        public CarritoController(ICarritoModel carritoModel, IPaquetesModel paquetesModel)
        {
            _carritoModel = carritoModel;
            _paquetesModel = paquetesModel;
        }

        [HttpGet]
        public IActionResult AgregarPaqueteCarrito(long q)
        {
            CarritoEnt entidad = new CarritoEnt();
            entidad.IdPaquete = q;

            var datos = _carritoModel.AgregarPaqueteCarrito(entidad);

            var datosCarrito = _carritoModel.ConsultarResumenCarrito();

            HttpContext.Session.SetString("Cantidad", datosCarrito?.Objeto.Cantidad.ToString());
            HttpContext.Session.SetString("SubTotal", datosCarrito?.Objeto.SubTotal.ToString());
            HttpContext.Session.SetString("Total", datosCarrito?.Objeto.Total.ToString());

            if (datos?.Codigo != 1)
            {
                ViewBag.Mensaje = datos?.Mensaje;
                HttpContext.Session.SetString("NombrePantalla", "Paquetes Disponibles");
                var paquetes = _paquetesModel.ConsultarPaquetes(false);
                return View("../Home/PantallaPrincipal", paquetes?.Objetos);
            }

            return RedirectToAction("PantallaPrincipal", "Home");
        }

        [HttpPost]
        public JsonResult RemoverPaqueteCarrito(long IdCarrito)
        {
            _carritoModel.RemoverPaqueteCarrito(IdCarrito);

            var datosCarrito = _carritoModel.ConsultarResumenCarrito();

            HttpContext.Session.SetString("Cantidad", datosCarrito?.Objeto.Cantidad.ToString());
            HttpContext.Session.SetString("SubTotal", datosCarrito?.Objeto.SubTotal.ToString());
            HttpContext.Session.SetString("Total", datosCarrito?.Objeto.Total.ToString());

            return Json("OK");
        }

        [HttpGet]
        public IActionResult ConsultarDetalleCarrito()
        {
            var datos = _carritoModel?.ConsultarDetalleCarrito();
            return View(datos?.Objetos);
        }

        [HttpPost]
        public IActionResult ConfirmarPago()
        {
            _carritoModel.ConfirmarPago();
            return RedirectToAction("PantallaPrincipal", "Home");
        }

    }
}
