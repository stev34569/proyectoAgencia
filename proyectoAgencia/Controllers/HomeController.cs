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
            var nombreUsuario = _usuariosModel.ValidarCredenciales("sbansbach60414@ufide.ac.cr", "123");

            HttpContext.Session.SetString("nombreUsuario", nombreUsuario);
            HttpContext.Session.GetString("nombreUsuario");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

    }
}