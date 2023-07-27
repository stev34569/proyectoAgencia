using Microsoft.AspNetCore.Mvc;

namespace proyectoAgencia.Controllers
{
    public class PaqueteController : Controller
    {
        [HttpGet]
        public IActionResult ConsultarPaquetesUsuario()
        {
            return View();
        }
    }
}
