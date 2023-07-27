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

        public PaqueteEntRespuesta? ConsultarPaquetes()
        {
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");
            string url = "/api/Paquete/ConsultarPaquetes";

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
      
    }
}
