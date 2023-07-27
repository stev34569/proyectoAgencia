using Microsoft.AspNetCore.Mvc;

namespace proyectoAgencia.Controllers
{
    public class Paquete2Controller : Controller
    {
        [HttpGet]
        public IActionResult ConsultarPaquetes2()
        {
            return View();
        }
    }
}
