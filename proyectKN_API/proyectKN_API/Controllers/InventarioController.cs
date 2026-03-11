/*
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using proyectKN_API.Models;

namespace proyectKN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly IConfiguration _config;

        public InventarioController(IConfiguration config)
        {
            _config = config;
        }
        //Registro Inventario
        [HttpPost("RegistroInventario")]
        public IActionResult RegistroInventario(InventarioRequest model)
        {
            using var context = new SqlConnection(
                _config.GetValue<string>("ConnectionStrings:DefaultConnection"));
            var parametros = new DynamicParameters();
            parametros.Add("@Nombre", model.Nombre);
            parametros.Add("@IdArticulo", model.IdArticulo);
            parametros.Add("@PrecioCompra", model.PrecioCompra);
            parametros.Add("@Stock", model.Stock);
            parametros.Add("@StockMinimo", model.StockMinimo);
            parametros.Add("@IdProveedor", model.IdProveedor);

            var result = context.Execute(
                "sp_RegostroInventario",
                parametros,
                commandType: System.Data.CommandType.StoredProcedure
                );
            if (result <= 0)
                return BadRequest("EL artículo no sé registró correctamente");
                    return Ok("El articulo se registro correctamente");
        }
    }
}
*/