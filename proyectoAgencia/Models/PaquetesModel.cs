using proyectoAgencia.Entities;
using System.Net.Http.Headers;
using System.Net.Http;
using proyectoAgencia.Interfaces;

namespace proyectoAgencia.Models
{
    public class PaquetesModel : IPaquetesModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private string _baseUrl;

        public PaquetesModel(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _baseUrl = _configuration.GetSection("Variables:urlBase").Value;
            _HttpContextAccessor = httpContextAccessor;
        }

        public PaqueteEntRespuesta? ConsultarPaquetes(bool MostraTodo)
        {
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");
            string url = "/api/Paquete/ConsultarPaquetes?MostraTodo=" + MostraTodo;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = _httpClient.GetAsync(_baseUrl + url).Result;
            return response.Content.ReadFromJsonAsync<PaqueteEntRespuesta>().Result;
        }

        public PaqueteEntRespuesta? ConsultarPaquete(long IdPaquete)
        {
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");
            string url = "/api/Paquete/ConsultarPaquete?IdPaquete=" + IdPaquete;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = _httpClient.GetAsync(_baseUrl + url).Result;
            return response.Content.ReadFromJsonAsync<PaqueteEntRespuesta>().Result;
        }

        public PaqueteEntRespuesta? ConsultarPaquetesUsuario()
        {
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");
            string url = "/api/Paquete/ConsultarPaquetesUsuario";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = _httpClient.GetAsync(_baseUrl + url).Result;
            return response.Content.ReadFromJsonAsync<PaqueteEntRespuesta>().Result;
        }

        public PaqueteEntRespuesta? RegistrarPaquete(PaqueteEnt entidad)
        {
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");
            string url = "/api/Paquete/RegistrarPaquete";
            JsonContent jsonObject = JsonContent.Create(entidad);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = _httpClient.PostAsync(_baseUrl + url, jsonObject).Result;
            return response.Content.ReadFromJsonAsync<PaqueteEntRespuesta>().Result;
        }

        public PaqueteEntRespuesta? ActualizarImagen(PaqueteEnt entidad)
        {
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");
            string url = "/api/Paquete/ActualizarImagen";
            JsonContent jsonObject = JsonContent.Create(entidad);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = _httpClient.PutAsync(_baseUrl + url, jsonObject).Result;
            return response.Content.ReadFromJsonAsync<PaqueteEntRespuesta>().Result;
        }

        public PaqueteEntRespuesta? ActualizarPaquete(PaqueteEnt entidad)
        {
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");
            string url = "/api/Paquete/ActualizarPaquete";
            JsonContent jsonObject = JsonContent.Create(entidad);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = _httpClient.PutAsync(_baseUrl + url, jsonObject).Result;
            return response.Content.ReadFromJsonAsync<PaqueteEntRespuesta>().Result;
        }

    }
}
