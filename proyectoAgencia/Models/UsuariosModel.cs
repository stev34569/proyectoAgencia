using proyectoAgencia.Entities;
using proyectoAgencia.Interfaces;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace proyectoAgencia.Models
{
    public class UsuariosModel : IUsuariosModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private string _baseUrl;

        public UsuariosModel(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _baseUrl = _configuration.GetSection("Variables:urlBase").Value;
            _HttpContextAccessor = httpContextAccessor;
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
            string url = "/api/Usuario/RecuperarContrasenna?CorreoElectronico=" + entidad.CorreoElectronico;
            var response = _httpClient.GetAsync(_baseUrl + url).Result;
            return response.Content.ReadFromJsonAsync<UsuarioEntRespuesta>().Result;
        }

        public UsuarioEntRespuesta? CambiarContrasenna(UsuarioEnt entidad)
        {
            string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");
            string url = "/api/Usuario/CambiarContrasenna";
            JsonContent jsonObject = JsonContent.Create(entidad);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = _httpClient.PutAsync(_baseUrl + url, jsonObject).Result;
            return response.Content.ReadFromJsonAsync<UsuarioEntRespuesta>().Result;
        }

        public string Encrypt(string toEncrypt)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            string key = "vJlg369z5KkIOoT41F3tgfFPvbwlrnJr";
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

    }
}