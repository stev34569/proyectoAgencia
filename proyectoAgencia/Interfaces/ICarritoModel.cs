using proyectoAgencia.Entities;

namespace proyectoAgencia.Interfaces
{
    public interface ICarritoModel
    {
        public CarritoEntRespuesta? AgregarPaqueteCarrito(CarritoEnt entidad);
        public CarritoEntRespuesta? RemoverPaqueteCarrito(long IdCarrito);
        public CarritoEntRespuesta? ConfirmarPago();
        public CarritoEntRespuesta? ConsultarResumenCarrito();
        public CarritoEntRespuesta? ConsultarDetalleCarrito();
    }
}
