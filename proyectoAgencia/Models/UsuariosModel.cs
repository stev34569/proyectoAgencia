using proyectoAgencia.Entities;
using proyectoAgencia.Interfaces;

namespace proyectoAgencia.Models
{
    public class UsuariosModel : IUsuariosModel
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string _baseUrl;


        public UsuariosModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _baseUrl = _configuration.GetSection("Variables:urlBase").Value;
        }


        public UsuarioEntRespuesta? IniciarSesion(UsuarioEnt entidad)
        {
            string url = "/api/Usuario/IniciarSesion";
            JsonContent jsonObject = JsonContent.Create(entidad);
            var response = _httpClient.PostAsync(_baseUrl + url, jsonObject).Result;
            return response.Content.ReadFromJsonAsync<UsuarioEntRespuesta>().Result;
        }

        public UsuarioEntRespuesta? RegistrarUsuario(UsuarioEnt entidad)
        {
            string url = "/api/Usuario/RegistrarUsuario";
            JsonContent jsonObject = JsonContent.Create(entidad);
            var response = _httpClient.PostAsync(_baseUrl + url, jsonObject).Result;
            return response.Content.ReadFromJsonAsync<UsuarioEntRespuesta>().Result;
        }

        public UsuarioEntRespuesta? RecuperarContrasenna(UsuarioEnt entidad)
        {
            string url = "/api/Usuario/ValidarCorreo?CorreoElectronico=" + entidad.CorreoElectronico;
            var response = _httpClient.GetAsync(_baseUrl + url).Result;
            return response.Content.ReadFromJsonAsync<UsuarioEntRespuesta>().Result;
        }

    }
}