using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using proyectoAgencia.Entities;
using proyectoAgencia.Interfaces;
using proyectoAgencia.Models;
using System.Diagnostics;
using System.Net;

namespace proyectoAgencia.Controllers
{

    public class HomeController : Controller
    {
        private readonly IUsuariosModel _usuariosModel;
        private readonly IPaquetesModel _paquetesModel;
        private readonly ICarritoModel _carritoModel;
        private readonly IConfiguration _configuration;



        public HomeController(IUsuariosModel usuariosModel, IPaquetesModel paquetesModel, ICarritoModel carritoModel, IConfiguration configuration)
        {
            _usuariosModel = usuariosModel;
            _paquetesModel = paquetesModel;
            _carritoModel = carritoModel;
            _configuration = configuration;

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
                HttpContext.Session.SetString("NombreUsuario", datos.Objeto.Nombre.ToString());
                HttpContext.Session.SetString("RolUsuario", datos.Objeto.IdRol.ToString());
                HttpContext.Session.SetString("NombreRolUsuario", datos.Objeto.NombreRol.ToString());

                if (datos?.Objeto.ContrasennaTemp == true)
                {
                    return RedirectToAction("Cambiar", "Home");
                }

                return RedirectToAction("PantallaPrincipal", "Home");

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Consulte con el administrador";
                return View("Index");
            }
        }



        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }


        public bool ValidarCaptcha(string captchaResponse)
        {
            var result = false;
            //secretkey que te dan igual en la pag de captcha
            var secretKey = _configuration["AppSettings:SecretKey"];
            //0 y 1 son marcadores de posicion, posteriormente son las que van a ser remplazados con el secretkey y el response
            var apiUrl = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";
            var requestUri = string.Format(apiUrl, secretKey, captchaResponse);
            //se crea la solicitud para ser enviada
            var request = (HttpWebRequest)WebRequest.Create(requestUri);
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    JObject jResponse = JObject.Parse(stream.ReadToEnd());
                    var isSuccess = jResponse.Value<bool>("success");
                    result = isSuccess;
                }
            }
            return result;

        }



        [HttpPost]
        public IActionResult RegistrarUsuario(UsuarioEnt entidad, string recaptchaValue)
        {
            // Verificar el reCAPTCHA
            if (string.IsNullOrEmpty(recaptchaValue))
            {
                ViewBag.Mensaje = "Por favor, complete el reCAPTCHA.";
                return View("Registro");
            }

            if (!ValidarCaptcha(recaptchaValue))
            {
                ViewBag.Mensaje = "Captcha no válido.";
                return View("Registro");
            }

            // El reCAPTCHA es válido, continuar con el registro del usuario
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
                ViewBag.Mensaje = "Las contraseñas no pueden ser iguales";
                return View("Cambiar");
            }

            var datos = _usuariosModel.CambiarContrasenna(entidad);
            if (datos?.Codigo != 1)
            {
                ViewBag.Mensaje = datos?.Mensaje;
                return View("Cambiar");
            }

            return RedirectToAction("PantallaPrincipal", "Home");
        }



        [HttpGet]
        public IActionResult CambiarContrasennaUsuario()
        {
            HttpContext.Session.SetString("NombrePantalla", "Cambiar Contraseña");
            return View();
        }

        [HttpPost]
        public IActionResult CambiarContrasennaUsuario(UsuarioEnt entidad)
        {
            entidad.Contrasenna = _usuariosModel.Encrypt(entidad.Contrasenna);
            entidad.ConfirmarContrasenna = _usuariosModel.Encrypt(entidad.ConfirmarContrasenna);

            if (entidad.Contrasenna != entidad.ConfirmarContrasenna)
            {
                ViewBag.Mensaje = "Las contraseñas no coinciden";
                return View("CambiarContrasennaUsuario");
            }

            var datos = _usuariosModel.CambiarContrasenna(entidad);
            if (datos?.Codigo != 1)
            {
                ViewBag.Mensaje = datos?.Mensaje;
                return View("CambiarContrasennaUsuario");
            }

            return RedirectToAction("PantallaPrincipal", "Home");
        }



        [HttpGet]
        public IActionResult PantallaPrincipal()
        {
            HttpContext.Session.SetString("NombrePantalla", "Paquetes Disponibles");
            var datos = _paquetesModel.ConsultarPaquetes();

            var datosCarrito = _carritoModel.ConsultarResumenCarrito();

            HttpContext.Session.SetString("Cantidad", datosCarrito?.Objeto.Cantidad.ToString());
            HttpContext.Session.SetString("SubTotal", datosCarrito?.Objeto.SubTotal.ToString());

            return View(datos?.Objetos);
        }



        [HttpGet]
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}