/*using Microsoft.AspNetCore.Mvc;
using proyectKN.Models;
using System.Net;

namespace proyectKN.Controllers
{
    public class InventarioController : Controller
    {
        private readonly IHttpClientFactory _http;
        private readonly IConfiguration _config; 

        public InventarioController(IHttpClientFactory http, IConfiguration config)
        {
            _http = http;
            _config = config;
        }


        // GET: Inventario/Registro
        public IActionResult Registro()
        {
            //using (var db = new TallerLaUnionContext())
            //{
            //    var productos = db.Productos.ToList();
            //    return View(productos);
            //}
            return View();
        }

        // POST: Inventario/Registro
        [HttpPost]
        public IActionResult Registro(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                using (var db = new TallerLaUnionContext())
                {
                    var productos = db.Productos.ToList();
                    return View(productos);
                }
            }

            using (var db = new TallerLaUnionContext())
            {
                db.Productos.Add(producto);
                db.SaveChanges();
            }

            return RedirectToAction("Registro");
        }

        // ELIMINAR
        public IActionResult Eliminar(string id)
        {
            using (var db = new TallerLaUnionContext())
            {
                var producto = db.Productos.FirstOrDefault(p => p.IdProducto == id);
                if (producto != null)
                {
                    db.Productos.Remove(producto);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Registro");
        }

        // EDITAR (GET)
        public IActionResult Editar(string id)
        {
            using (var db = new TallerLaUnionContext())
            {
                var producto = db.Productos.FirstOrDefault(p => p.IdProducto == id);
                if (producto == null)
                    return RedirectToAction("Registro");

                return View(producto);
            }
        }

        // EDITAR (POST)
        [HttpPost]
        public IActionResult Editar(Producto producto)
        {
            if (!ModelState.IsValid)
                return View(producto);

            using (var db = new TallerLaUnionContext())
            {
                var productoDb = db.Productos.FirstOrDefault(p => p.IdProducto == producto.IdProducto);
                if (productoDb == null)
                    return RedirectToAction("Registro");

                productoDb.Nombre = producto.Nombre;
                productoDb.Stock = producto.Stock;
                productoDb.PrecioCompra = producto.PrecioCompra;
                productoDb.PrecioVenta = producto.PrecioVenta;
                productoDb.Descripcion = producto.Descripcion;

                db.SaveChanges();
            }

            return RedirectToAction("Registro");
        }
       
    }
}
*/