using proyectoAgencia.Entities;

namespace proyectoAgencia.Interfaces
{
    public interface IUsuariosModel
    {

        public UsuarioEntRespuesta? IniciarSesion(UsuarioEnt entidad);
        public UsuarioEntRespuesta? RegistrarUsuario(UsuarioEnt entidad);
        public UsuarioEntRespuesta? RecuperarContrasenna(UsuarioEnt entidad);
    }
}