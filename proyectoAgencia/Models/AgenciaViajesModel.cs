using proyectoAgencia.Entities;
using proyectoAgencia.Interfaces;
using NuGet.Common;
using System.Configuration;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;

namespace proyectoAgencia.Models
{
    public class AgenciaViajesModel : IAgenciaViajesModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private string _baseUrl;

        public AgenciaViajesModel(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _baseUrl = _configuration.GetSection("Variables:urlBase").Value;
            _HttpContextAccessor = httpContextAccessor;
        }
        public AgenciaViajesEntRespuesta? RegistrarAgenciaViajes(AgenciaViajesEnt entidad)
        {
            string url = "/api/AgenciaViajes/RegistrarAgenciaViajes";
            JsonContent jsonObject = JsonContent.Create(entidad);
            var response = _httpClient.PostAsync(_baseUrl + url, jsonObject).Result;
            return response.Content.ReadFromJsonAsync<AgenciaViajesEntRespuesta>().Result;
        }

        public AgenciaViajesEntRespuesta? ConsultarAgenciaViajes()
        {
           
            string url = "/api/AgenciaViajes/ConsultarAgenciaViajes";
            var response = _httpClient.GetAsync(_baseUrl + url).Result;
            return response.Content.ReadFromJsonAsync<AgenciaViajesEntRespuesta>().Result;
        }

        public AgenciaViajesEntRespuesta? CambiarEstado(AgenciaViajesEnt entidad)
        {
            string url = "/api/AgenciaViajes/CambiarEstado";
            JsonContent jsonObject = JsonContent.Create(entidad);
            var response = _httpClient.PutAsync(_baseUrl + url, jsonObject).Result;
            return response.Content.ReadFromJsonAsync<AgenciaViajesEntRespuesta>().Result;
        }

        public AgenciaViajesEntRespuesta? ConsultarAgenciaViaje(long q)
        {
            string url = "/api/AgenciaViajes/ConsultarAgenciaViaje?q=" + q;
            var response = _httpClient.GetAsync(_baseUrl + url).Result;
            return response.Content.ReadFromJsonAsync<AgenciaViajesEntRespuesta>().Result;
        }

        public AgenciaViajesEntRespuesta? EditarAgencia(AgenciaViajesEnt entidad)
        {
           
            string url = "/api/AgenciaViajes/EditarAgencia";
            JsonContent jsonObject = JsonContent.Create(entidad);
            var response = _httpClient.PutAsync(_baseUrl + url, jsonObject).Result;
            return response.Content.ReadFromJsonAsync<AgenciaViajesEntRespuesta>().Result;
        }

    }
}
