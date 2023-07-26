using proyectoAgencia.Entities;

namespace proyectoAgencia.Interfaces
{
    public interface IAgenciaViajesModel
    {
        public AgenciaViajesEntRespuesta? RegistrarAgenciaViajes(AgenciaViajesEnt entidad);
        public AgenciaViajesEntRespuesta? ConsultarAgenciaViajes();
        public AgenciaViajesEntRespuesta? CambiarEstado(AgenciaViajesEnt entidad);

        public AgenciaViajesEntRespuesta? ConsultarAgenciaViaje(long q);
    }
}
