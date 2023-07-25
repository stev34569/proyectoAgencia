
using Microsoft.AspNetCore.Mvc;
using proyectoAgencia.Entities;
using proyectoAgencia.Interfaces;
using proyectoAgencia.Models;
using System.Diagnostics;

namespace proyectoAgencia.Controllers
{

    public class HomeController : Controller
    {
        private readonly IUsuariosModel _usuariosModel;

        public HomeController(IUsuariosModel usuariosModel)
        {
            _usuariosModel = usuariosModel;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IniciarSesion(UsuarioEnt entidad)
        {
            try
            {
                entidad.Contrasenna = _usuariosModel.Encrypt(entidad.Contrasenna);
                var datos = _usuariosModel.IniciarSesion(entidad);
                if (datos?.Codigo != 1)
                {
                    ViewBag.Mensaje = datos?.Mensaje;
                    return View("Index");
                }

                HttpContext.Session.SetString("TokenUsuario", datos.Objeto.Token.ToString());

                if (datos?.Objeto.ContrasennaTemp == true)
                {
                    return RedirectToAction("Cambiar", "Home");
                }

                return RedirectToAction("PantallaPrincipal", "Home");

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Consulte con un agente de viajes";
                return View("Index");
            }
        }


        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarUsuario(UsuarioEnt entidad)
        {
            entidad.Contrasenna = _usuariosModel.Encrypt(entidad.Contrasenna);
            var datos = _usuariosModel.RegistrarUsuario(entidad);
            if (datos?.Codigo != 1)
            {
                ViewBag.Mensaje = datos?.Mensaje;
                return View("Registro");
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Recuperar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecuperarContrasenna(UsuarioEnt entidad)
        {
            var datos = _usuariosModel.RecuperarContrasenna(entidad);
            if (datos?.Codigo != 1)
            {
                ViewBag.Mensaje = datos?.Mensaje;
                return View("Recuperar");
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Cambiar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CambiarContrasenna(UsuarioEnt entidad)
        {
            entidad.Contrasenna = _usuariosModel.Encrypt(entidad.Contrasenna);

            if (entidad.Contrasenna == entidad.ContrasennaTemporal)
            {
                //Mensajes relevantes 
                ViewBag.Mensaje = "Las contraseñas deben ser difrentes ";
                return View("Cambiar");
            }

            var datos = _usuariosModel.CambiarContrasenna(entidad);
            if (datos?.Codigo != 1)
            {
                ViewBag.Mensaje = datos?.Mensaje;
                return View("Cambiar");
            }

            //Lo mande a la pantalla principal cuando cambiar la contraseña
            return RedirectToAction("PantallaPrincipal", "Home");
        }


        [HttpGet]
        public IActionResult PantallaPrincipal()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
