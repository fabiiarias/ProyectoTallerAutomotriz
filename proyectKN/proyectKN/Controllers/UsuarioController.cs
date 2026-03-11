using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using proyectKN.Models;
using proyectKN.Services;
using System.Net;

namespace proyectKN.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IHttpClientFactory _http;
        private readonly IConfiguration _config;
        private readonly IPasswordHelper _password;
        public UsuarioController(IHttpClientFactory http, IConfiguration config, IPasswordHelper password)
        {
            _http = http;
            _config = config;
            _password = password;
        }


        // GET REGISTRO USUARIO
        [HttpGet]
        public IActionResult RegistroUsuario()
        {
      
            using var client = _http.CreateClient();

            var urlRoles = _config.GetValue<string>("Valores:UrlAPI") + "Rol/ObtenerRoles";
            var urlEstados = _config.GetValue<string>("Valores:UrlAPI") + "Estado/ObtenerEstado";
            var urlUsuarios = _config.GetValue<string>("Valores:UrlAPI") + "Usuario/ConsultarUsuarios";

            // ROL
            var responseRoles = client.PostAsync(urlRoles, null).Result;

            if (responseRoles.IsSuccessStatusCode)
            {
                ViewBag.Roles = responseRoles.Content
                                             .ReadFromJsonAsync<List<Rol>>()
                                             .Result;
            }
            else
            {
                ViewBag.Roles = new List<Rol>();
            }

            // ESTADO
            var responseEstados = client.PostAsync(urlEstados, null).Result;

            if (responseEstados.IsSuccessStatusCode)
            {
                ViewBag.Estado = responseEstados.Content
                                                 .ReadFromJsonAsync<List<EstadoM>>()
                                                 .Result;
            }
            else
            {
                ViewBag.Estado = new List<EstadoM>();
            }

            //USUARIOS

            var responseUsuarios = client.PostAsync(urlUsuarios, null).Result;
            if (responseUsuarios.IsSuccessStatusCode)
            {
                ViewBag.Usuarios = responseUsuarios.Content
                                                    .ReadFromJsonAsync<List<UsuarioConsulta>>()
                                                    .Result;
            }
            else
            {
                ViewBag.Usuarios = new List<UsuarioConsulta>();
            }
            return View();
        }




        // POST REGISTRO USUARIO
        [HttpPost]
        public IActionResult RegistroUsuario(Usuario model)
        {
            model.Contrasenna = _password.Encrypt(model.Contrasenna);
            using var client = _http.CreateClient();
            var url = _config.GetValue<string>("Valores:UrlAPI") + "Usuario/RegistroUsuario";
            var result = client.PostAsJsonAsync(url, model).Result;

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("RegistroUsuario", "Usuario");
            }
            else if (result.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new Exception();
            }
            ViewBag.Mensaje = result.Content.ReadAsStringAsync().Result;
            return RegistroUsuario();
        }

        //GET EDITAR 

        [HttpGet]
        public IActionResult EditarUsuario(int id)
        {
            using var client = _http.CreateClient();

            // Traer Roles y Estados
            ViewBag.Roles = client.PostAsync(_config.GetValue<string>("Valores:UrlAPI") + "Rol/ObtenerRoles", null)
                                  .Result.Content.ReadFromJsonAsync<List<Rol>>().Result;
            ViewBag.Estado = client.PostAsync(_config.GetValue<string>("Valores:UrlAPI") + "Estado/ObtenerEstado", null)
                                   .Result.Content.ReadFromJsonAsync<List<EstadoM>>().Result;

            // Traer Usuario por ID
            var responseUsuario = client.GetAsync(_config.GetValue<string>("Valores:UrlAPI") + $"Usuario/ObtenerUsuarioId/{id}").Result;

            Usuario usuario = null;
            if (responseUsuario.IsSuccessStatusCode)
            {
                usuario = responseUsuario.Content.ReadFromJsonAsync<Usuario>().Result;
            }
            else
            {
                TempData["Mensaje"] = "No se pudo cargar el usuario.";
                return RedirectToAction("RegistroUsuario");
            }

            return View(usuario);
        }

        [HttpPost]
        public IActionResult EditarUsuario(Usuario model)
        {
            using var client = _http.CreateClient();

            if (string.IsNullOrWhiteSpace(model.Contrasenna))
                model.Contrasenna = null;
 

            var response = client.PostAsJsonAsync(_config.GetValue<string>("Valores:UrlAPI") + "Usuario/EditarUsuario", model).Result;

            if (response.IsSuccessStatusCode)
                TempData["Mensaje"] = "Usuario actualizado correctamente";
            else
                TempData["Mensaje"] = "No se pudo actualizar el usuario";

            return RedirectToAction("RegistroUsuario");
        }
    }
}


