using proyectoAgencia.Entities;
using proyectoAgencia.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;

namespace proyectoAgencia.Models
{
    public class CarritoModel : ICarritoModel
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private string _baseUrl;

        public CarritoModel(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _baseUrl = _configuration.GetSection("Variables:urlBase").Value;
            _HttpContextAccessor = httpContextAccessor;
        }

        public CarritoEntRespuesta? AgregarPaqueteCarrito(CarritoEnt entidad)
        {
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");
            string url = "/api/Carrito/AgregarPaqueteCarrito";
            JsonContent jsonObject = JsonContent.Create(entidad);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = _httpClient.PostAsync(_baseUrl + url, jsonObject).Result;
            return response.Content.ReadFromJsonAsync<CarritoEntRespuesta>().Result;
        }

        public CarritoEntRespuesta? RemoverPaqueteCarrito(long IdCarrito)
        {
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");
            string url = "/api/Carrito/RemoverPaqueteCarrito?IdCarrito=" + IdCarrito;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = _httpClient.DeleteAsync(_baseUrl + url).Result;
            return response.Content.ReadFromJsonAsync<CarritoEntRespuesta>().Result;
        }

        public CarritoEntRespuesta? ConfirmarPago()
        {
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");
            string url = "/api/Carrito/ConfirmarPago";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = _httpClient.PostAsync(_baseUrl + url, null).Result;
            return response.Content.ReadFromJsonAsync<CarritoEntRespuesta>().Result;
        }

        public CarritoEntRespuesta? ConsultarResumenCarrito()
        {
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");
            string url = "/api/Carrito/ConsultarResumenCarrito";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = _httpClient.GetAsync(_baseUrl + url).Result;
            return response.Content.ReadFromJsonAsync<CarritoEntRespuesta>().Result;
        }

        public CarritoEntRespuesta? ConsultarDetalleCarrito()
        {
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");
            string url = "/api/Carrito/ConsultarDetalleCarrito";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = _httpClient.GetAsync(_baseUrl + url).Result;
            return response.Content.ReadFromJsonAsync<CarritoEntRespuesta>().Result;
        }

    }
}
