/*
using Microsoft.AspNetCore.Mvc;
using proyectKN.Models.DB;
using System.Linq;

namespace proyectKN.Controllers
{
    public class VehiculoController : Controller
    {
        public string? Cedula { get; private set; }

        // GET: Vehiculo/RegistroVehicular
        public IActionResult RegistroVehiculo()
        {
            //    using (var db = new TallerLaUnionContext())
            //    {
            //        var vehiculos = db.Vehiculos.ToList();
            //        return View(vehiculos);
            //    }
            return View();
            }
        
        // POST: Vehiculo/RegistroVehicular
        [HttpPost]
        public IActionResult RegistroVehiculo(Vehiculo vehiculo)
        {
            //    if (!ModelState.IsValid)
            //    {
            //        using (var db = new TallerLaUnionContext())
            //        {
            //            var vehiculos = db.Vehiculos.ToList();
            //            return View(vehiculos);
            //        }
            //    }

            //    using (var db = new TallerLaUnionContext())
            //    {
            //        db.Vehiculos.Add(vehiculo);
            //        db.SaveChanges();
            //    }

            //    return RedirectToAction("RegistroVehiculo");
            return View();
        }

        // ELIMINAR
        public IActionResult Eliminar(int id)
        {
            using (var db = new TallerLaUnionContext())
            {
                var vehiculo = db.Vehiculos.FirstOrDefault(v => v.IdVehiculo == id);
                if (vehiculo != null)
                {
                    db.Vehiculos.Remove(vehiculo);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("RegistroVehiculo");
        }

        // EDITAR (GET)
        public IActionResult Editar(int id)
        {
            using (var db = new TallerLaUnionContext())
            {
                var vehiculo = db.Vehiculos.FirstOrDefault(v => v.IdVehiculo == id);
                if (vehiculo == null)
                    return RedirectToAction("RegistroVehiculo");

                return View(vehiculo); // aquí busca Editar.cshtml
            }
        }


        // EDITAR (POST)
        [HttpPost]
        public IActionResult Editar(Vehiculo vehiculo)
        {
            if (!ModelState.IsValid)
                return View(vehiculo);

            using (var db = new TallerLaUnionContext())
            {
                var vehiculoDb = db.Vehiculos.FirstOrDefault(v => v.IdVehiculo == vehiculo.IdVehiculo);
                if (vehiculoDb == null)
                    return RedirectToAction("RegistroVehiculo");

                vehiculoDb.Marca = vehiculo.Marca;
                vehiculoDb.Modelo = vehiculo.Modelo;
                vehiculoDb.Anio = vehiculo.Anio;
                vehiculoDb.Placa = vehiculo.Placa;
                vehiculoDb.Estado = vehiculo.Estado;
                vehiculoDb.Problema = vehiculo.Problema;
                vehiculoDb.NombreCliente = vehiculo.NombreCliente;
                vehiculoDb.Telefono = vehiculo.Telefono;
                vehiculoDb.Cedula = vehiculo.Cedula;

                db.SaveChanges();
            }

            return RedirectToAction("RegistroVehiculo");
        }
    }
}

*/