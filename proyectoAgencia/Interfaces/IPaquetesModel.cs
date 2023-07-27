using proyectoAgencia.Entities;


namespace proyectoAgencia.Interfaces
{
    public interface IPaquetesModel
    {

        public PaqueteEntRespuesta? ConsultarPaquetes();

        public PaqueteEntRespuesta? ConsultarPaquetesUsuario();

    }
}
