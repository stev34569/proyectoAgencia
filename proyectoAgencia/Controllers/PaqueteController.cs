using Microsoft.AspNetCore.Mvc;
using proyectoAgencia.Entities;
using proyectoAgencia.Interfaces;
using proyectoAgencia.Models;

namespace proyectoAgencia.Controllers
{

    [FiltroSesion]
    [ResponseCache(NoStore = true, Duration = 0)]
    public class PaqueteController : Controller
    {
        private readonly IPaquetesModel _paquetesModel;
        private IHostEnvironment _hostingEnvironment;
        private readonly IBitacoraModel _bitacoraModel;

        public PaqueteController(IPaquetesModel cursosModel, IHostEnvironment hostingEnvironment, IBitacoraModel bitacoraModel)
        {
            _paquetesModel = cursosModel;
            _hostingEnvironment = hostingEnvironment;
            _bitacoraModel = bitacoraModel;
        }

        [HttpGet]
        public IActionResult ConsultarPaquetesUsuario()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ConsultarPaquetesUsuarioAjax()
        {
            var datos = _paquetesModel.ConsultarPaquetesUsuario();
            return Json(datos.Objetos);
        }

        [HttpGet]
        public IActionResult ConsultarMantPaquetes()
        {
            var datos = _paquetesModel.ConsultarPaquetes(true);
            return View(datos.Objetos);
        }

        [HttpGet]
        public IActionResult AgregarPaquetes()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AgregarPaquetes(PaqueteEnt entidad, IFormFile ImagenPaquete)
        {
            try
            {
                string extension = Path.GetExtension(Path.GetFileName(ImagenPaquete.FileName));
                string folder = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot\\images");

                // Registrar Paquete para tomar el ID del Paquete
                var datos = _paquetesModel.RegistrarPaquete(entidad);

                // Copiamos la imagen en un folder del proyecto con el nombre del ID del Paquete
                string archivo = Path.Combine(folder, datos.Objeto.IdPaquete + extension);

                using (Stream fileStream = new FileStream(archivo, FileMode.Create))
                {
                    ImagenPaquete.CopyToAsync(fileStream);
                }

                // Actualizamos la ruta de la imagen del Paquete
                entidad.Imagen = "\\images\\" + datos.Objeto.IdPaquete + extension;
                entidad.IdPaquete = datos.Objeto.IdPaquete;
                _paquetesModel.ActualizarImagen(entidad);

                return RedirectToAction("ConsultarMantPaquetes", "Paquete");
            }
            catch (Exception ex)
            {
                BitacoraEnt bitacora = new BitacoraEnt();
                bitacora.Origen = ControllerContext.ActionDescriptor.ControllerName + " - " + ControllerContext.ActionDescriptor.ActionName;
                bitacora.Mensaje = ex.Message;
                bitacora.DireccionIP = HttpContext.Connection.RemoteIpAddress.ToString();
                _bitacoraModel.RegistrarBitacora(bitacora);
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult EditarPaquete(long IdPaquete)
        {
            try
            {
                var datos = _paquetesModel.ConsultarPaquete(IdPaquete);
                if (datos?.Codigo != 1)
                {
                    // Manejar el error de consulta, si es necesario
                    ViewBag.Mensaje = datos?.Mensaje;
                    return View("Error");
                }

                datos.Objeto.Imagen = datos.Objeto.Imagen.Replace("\\images\\", "");
                return View(datos.Objeto);
            }
            catch (Exception ex)
            {
                BitacoraEnt bitacora = new BitacoraEnt();
                bitacora.Origen = ControllerContext.ActionDescriptor.ControllerName + " - " + ControllerContext.ActionDescriptor.ActionName;
                bitacora.Mensaje = ex.Message;
                bitacora.DireccionIP = HttpContext.Connection.RemoteIpAddress.ToString();
                _bitacoraModel.RegistrarBitacora(bitacora);
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult EditarPaquetes(PaqueteEnt entidad, IFormFile ImagenPaquete)
        {
            string extension = Path.GetExtension(Path.GetFileName(ImagenPaquete.FileName));
            string folder = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot\\images");

            System.IO.File.Delete(Path.Combine(folder, entidad.Imagen));

            //Registrar Paquete para tomar el ID del Paquete
            _paquetesModel.ActualizarPaquete(entidad);

            //Copiamos la imagen en un folder del proyecto con el nombre del ID del Paquete
            string archivo = Path.Combine(folder, entidad.IdPaquete + extension);
            using (Stream fileStream = new FileStream(archivo, FileMode.Create))
            {
                ImagenPaquete.CopyToAsync(fileStream);
            }

            //Actualizamos la ruta de la imagen del Paquete
            entidad.Imagen = "\\images\\" + entidad.IdPaquete + extension;
            _paquetesModel.ActualizarImagen(entidad);

            return RedirectToAction("ConsultarMantPaquetes", "Paquete");
        }

    }
}

