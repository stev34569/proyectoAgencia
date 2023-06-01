using Microsoft.AspNetCore.Mvc;
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
        public IActionResult IniciarSesion()
        {
            /*PROGRA*/
            return RedirectToAction("PantallaPrincipal", "Home");
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarUsuario()
        {
            return View();
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