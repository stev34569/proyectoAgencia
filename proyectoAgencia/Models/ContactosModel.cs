using proyectoAgencia.Entities;
using proyectoAgencia.Interfaces;
using System.Net.Http.Headers;

namespace proyectoAgencia.Models
{

    public class ContactosModel : IContactosModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private string _baseUrl;

        public ContactosModel(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _baseUrl = _configuration.GetSection("Variables:urlBase").Value;
            _HttpContextAccessor = httpContextAccessor;
        }

        public ContactosEntRespuesta? RegistrarContacto(ContactosEnt entidad)
        {
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");
            string url = "/api/Contactos/RegistrarContactos";
            JsonContent jsonObject = JsonContent.Create(entidad);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = _httpClient.PostAsync(_baseUrl + url, jsonObject).Result;
            return response.Content.ReadFromJsonAsync<ContactosEntRespuesta>().Result;
        }

        public ContactosEntRespuesta? ConsultarContactos()
        {
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");
            string url = "/api/Contactos/ConsultarContactos";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = _httpClient.GetAsync(_baseUrl + url).Result;
            return response.Content.ReadFromJsonAsync<ContactosEntRespuesta>().Result;
        }

        public ContactosEntRespuesta? CambiarEstado(ContactosEnt entidad)
        {
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");
            string url = "/api/Contactos/CambiarEstado";
            JsonContent jsonObject = JsonContent.Create(entidad);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = _httpClient.PutAsync(_baseUrl + url, jsonObject).Result;
            return response.Content.ReadFromJsonAsync<ContactosEntRespuesta>().Result;
        }



    }
}