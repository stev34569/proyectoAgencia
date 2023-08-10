using proyectoAgencia.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using proyectoAgencia.Interfaces;

namespace proyectoAgecia.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuariosModel _usuariosModel;

        public UsuarioController(IUsuariosModel usuariosModel)
        {
            _usuariosModel = usuariosModel;
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
            var datos = _usuariosModel.ConsultarUsuario(q);
            var roles = _usuariosModel.ConsultarRoles();

            var rolesDropDown = new List<SelectListItem>();
            foreach (var item in roles.Objetos)
            {
                rolesDropDown.Add(new SelectListItem { Value = item.IdRol.ToString(), Text = item.NombreRol });
            }

            ViewBag.rolesDropDown = rolesDropDown;
            return View(datos?.Objeto);
        }

    }
}

 