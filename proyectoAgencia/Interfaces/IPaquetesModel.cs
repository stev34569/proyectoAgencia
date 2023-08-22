using proyectoAgencia.Entities;


namespace proyectoAgencia.Interfaces
{
    public interface IPaquetesModel
    {
        public PaqueteEntRespuesta? ConsultarPaquetes(bool MostraTodo);

        public PaqueteEntRespuesta? ConsultarPaquete(long IdPaquete);

        public PaqueteEntRespuesta? ConsultarPaquetesUsuario();

        public PaqueteEntRespuesta? RegistrarPaquete(PaqueteEnt entidad);

        public PaqueteEntRespuesta? ActualizarImagen(PaqueteEnt entidad);

        public PaqueteEntRespuesta? ActualizarPaquete(PaqueteEnt entidad);

    }
}
