using Dapper;
using proyectKN_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
namespace proyectKN_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IConfiguration _config;

    public UsuarioController(IConfiguration config)
    {
        _config = config;
    }

    //  REGISTRO USUARIO
    [HttpPost("RegistroUsuario")]
    public IActionResult RegistroUsuario(UsuarioRequest model)
    {
        using var context = new SqlConnection(
            _config.GetValue<string>("ConnectionStrings:DefaultConnection"));

        var parametros = new DynamicParameters();
        parametros.Add("@NombreCompleto", model.NombreCompleto);
        parametros.Add("@Cedula", model.Cedula);
        parametros.Add("@Correo", model.Correo);
        parametros.Add("@UsuarioLogin", model.UsuarioLogin);
        parametros.Add("@Contrasenna", model.Contrasenna);
        parametros.Add("@Estado", model.Estado);
        parametros.Add("@NombreRol", model.NombreRol);

        var result = context.QueryFirst<int>(
            "sp_RegistroUsuario",
            parametros,
            commandType: System.Data.CommandType.StoredProcedure
        );

        if (result == -1)
            return BadRequest("La cédula ya está registrada");

        if (result == -2)
            return BadRequest("El usuario ya está registrado");

        return Ok("Usuario registrado correctamente");
    }

    //  CONSULTA USUARIOS
    [HttpPost("ConsultarUsuarios")]
    public IActionResult ConsultarUsuarios()
    {
        using var context = new SqlConnection(
            _config.GetConnectionString("DefaultConnection"));

        var lista = context.Query<UsuarioResponse>(
            "sp_ConsultarUsuarios",
            commandType: System.Data.CommandType.StoredProcedure
        ).ToList();

        if (!lista.Any())
            return NotFound("No hay usuarios registrados");

        return Ok(lista);
    }
    //CONSULTAR AL PROFE SI PUEDO USAR GET 
    //GET ACTUALIZAR USUARIO
    [HttpPost("EditarUsuario")]
    public IActionResult EditarUsuario([FromBody] UsuarioRequest model)
    {
        if (model.Consecutivo <= 0)
            return BadRequest("El ID no es válido");

        using var context = new SqlConnection(
            _config.GetConnectionString("DefaultConnection"));

        var parametros = new DynamicParameters();
        parametros.Add("@Consecutivo", model.Consecutivo);
        parametros.Add("@NombreCompleto", model.NombreCompleto);
        parametros.Add("@Cedula", model.Cedula);
        parametros.Add("@Correo", model.Correo);
        parametros.Add("@UsuarioLogin", model.UsuarioLogin);
        parametros.Add("@Estado", model.Estado);
        parametros.Add("@NombreRol", model.NombreRol);

        parametros.Add("@Contrasenna",
       string.IsNullOrWhiteSpace(model.Contrasenna) ? null : model.Contrasenna);

        var result = context.Execute(
            "sp_EditarUsuario",
            parametros,
            commandType: System.Data.CommandType.StoredProcedure
        );

        if (result <= 0)
            return BadRequest("No se pudo actualizar el usuario");

        return Ok("Usuario actualizado correctamente");
    }

    [HttpGet("ObtenerUsuarioId/{id}")]
    public IActionResult ObtenerUsuarioId(int id)
    {
        using var context = new SqlConnection(
            _config.GetConnectionString("DefaultConnection"));

        var parametros = new DynamicParameters();
        parametros.Add("@Consecutivo", id);

        var usuario = context.QueryFirstOrDefault<UsuarioResponse>(
            "sp_ObtenerId",
            parametros,
            commandType: System.Data.CommandType.StoredProcedure
        );

        if (usuario == null)
            return NotFound("Usuario no encontrado");

        return Ok(usuario);
    }
}