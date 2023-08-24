using proyectoAgencia.Entities;

namespace proyectoAgencia.Interfaces
{
    public interface IContactosModel
    {
        public ContactosEntRespuesta? RegistrarContacto(ContactosEnt entidad);

        public ContactosEntRespuesta? CambiarEstado(ContactosEnt entidad);

        public ContactosEntRespuesta? ConsultarContactos();
    }
}
