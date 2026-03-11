using Dapper;
using proyectKN_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
namespace proyectKN_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EstadoController : ControllerBase
{
    private readonly IConfiguration _config;
    public EstadoController(IConfiguration config)
    {
        _config = config;
    }
    [HttpPost("ObtenerEstado")]
    public IActionResult ObtenerEstado()
    {
        using var context = new SqlConnection(
            _config.GetValue<string>("ConnectionStrings:DefaultConnection"));

        var result = context.Query<EstadoM>(
            "sp_ObtenerEstado",
            commandType: System.Data.CommandType.StoredProcedure
        ).ToList();

        if (result == null || !result.Any())
            return NotFound("No hay estados registrados");

        return Ok(result);
    }
}
