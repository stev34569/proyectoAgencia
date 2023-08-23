using proyectoAgencia.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using proyectoAgencia.Interfaces;
using proyectoAgencia.Controllers;

namespace proyectoAgecia.Controllers
{

    [FiltroSesion]
    [ResponseCache(NoStore = true, Duration = 0)]
    public class UsuarioController : Controller
    {
        private readonly IUsuariosModel _usuariosModel;
        private readonly IBitacoraModel _bitacoraModel;

        public UsuarioController(IUsuariosModel usuariosModel, IBitacoraModel bitacoraModel)
        {
            _usuariosModel = usuariosModel;
            _bitacoraModel = bitacoraModel;
        }


        [HttpGet]
        public IActionResult ConsultarUsuarios()
        {
            var datos = _usuariosModel.ConsultarUsuarios();
            return View(datos?.Objetos);
        }


        [HttpGet]
        public IActionResult CambiarEstado(long q)
        {
            UsuarioEnt entidad = new UsuarioEnt();
            entidad.IdUsuario = q;

            var datos = _usuariosModel.CambiarEstado(entidad);
            if (datos?.Codigo != 1)
            {
                ViewBag.Mensaje = datos?.Mensaje;
                return View("ConsultarUsuarios");
            }

            return RedirectToAction("ConsultarUsuarios", "Usuario");
        }


        [HttpGet]
        public IActionResult Editar(long q)
        {
            try
            {
                var datos = _usuariosModel.ConsultarUsuario(q);
                var roles = _usuariosModel.ConsultarRoles();

                if (roles?.Codigo != 3)
                {
                    var rolesDropDown = new List<SelectListItem>();
                    foreach (var item in roles.Objetos)
                    {
                        rolesDropDown.Add(new SelectListItem { Value = item.IdRol.ToString(), Text = item.NombreRol });
                    }

                    ViewBag.rolesDropDown = rolesDropDown;
                    return View(datos?.Objeto);
                }
                else
                {
                    throw new Exception(roles?.Mensaje);
                }
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
        public IActionResult Editar(UsuarioEnt entidad)
        {
            var datos = _usuariosModel.EditarUsuario(entidad);
            if (datos?.Codigo != 1)
            {
                ViewBag.Mensaje = datos?.Mensaje;
                return View("Editar");
            }

            return RedirectToAction("ConsultarUsuarios", "Usuario");
        }

        [HttpPost]
        public IActionResult Editar(UsuarioEnt entidad)
        {
            var datos = _usuariosModel.EditarUsuario(entidad);
            if (datos?.Codigo != 1)
            {
                ViewBag.Mensaje = datos?.Mensaje;
                return View("Editar");
            }

            return RedirectToAction("ConsultarUsuarios", "Usuario");
        }

    }
}

