using proyectoAgencia.Entities;
using proyectoAgencia.Interfaces;
using System.Net.Http.Headers;

namespace proyectoAgencia.Models
{
    public class BitacoraModel : IBitacoraModel
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private string _baseUrl;

        public BitacoraModel(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _baseUrl = _configuration.GetSection("Variables:urlBase").Value;
            _HttpContextAccessor = httpContextAccessor;
        }

        public void RegistrarBitacora(BitacoraEnt entidad)
        {
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");
            string url = "/api/Bitacora/RegistrarBitacora";
            JsonContent jsonObject = JsonContent.Create(entidad);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = _httpClient.PostAsync(_baseUrl + url, jsonObject).Result;
        }

    }
}
