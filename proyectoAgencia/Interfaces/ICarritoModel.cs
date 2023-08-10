using proyectoAgencia.Entities;

namespace proyectoAgencia.Interfaces
{
    public interface ICarritoModel
    {
        public CarritoEntRespuesta? AgregarPaqueteCarrito(CarritoEnt entidad);
        public CarritoEntRespuesta? ConsultarResumenCarrito();
    }
}
