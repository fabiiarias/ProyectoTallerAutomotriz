/* using Microsoft.AspNetCore.Mvc;
using proyectKN.Models.DB;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace proyectKN.Controllers
{
    public class ContavilidadController : Controller
    {
        // GET
        public IActionResult RegistroIngreso()
        {
            //using (var db = new TallerLaUnionContext())
            //{
            //    var citas = db.Citas.ToList();
            //    return View(citas);
            //}
            return View();
        }

        // POST - CREAR
        [HttpPost]
        public IActionResult RegistroIngreso(Cita cita)
        {
            //using (var db = new TallerLaUnionContext())
            //{
            //    db.Citas.Add(cita);
            //    db.SaveChanges();
            //}

            //return RedirectToAction("RegistroCita");
            return View();
        }

        // ELIMINAR
        public IActionResult Eliminar(int id)
        {
            using (var db = new TallerLaUnionContext())
            {
                var cita = db.Citas.FirstOrDefault(c => c.IdCita == id);
                if (cita != null)
                {
                    db.Citas.Remove(cita);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("RegistroCita");
        }

        // EDITAR GET
        public IActionResult Editar(int id)
        {
            using (var db = new TallerLaUnionContext())
            {
                var cita = db.Citas.FirstOrDefault(c => c.IdCita == id);
                if (cita == null)
                    return RedirectToAction("RegistroCita");

                return View(cita);
            }
        }

        // EDITAR POST
        [HttpPost]
        public IActionResult Editar(Cita cita)
        {
            using (var db = new TallerLaUnionContext())
            {
                var citaDb = db.Citas.FirstOrDefault(c => c.IdCita == cita.IdCita);
                if (citaDb == null)
                    return RedirectToAction("RegistroCita");

                citaDb.NombreCliente = cita.NombreCliente;
                citaDb.Cedula = cita.Cedula;
                citaDb.FechaCita = cita.FechaCita;
                citaDb.HoraCita = cita.HoraCita;
                citaDb.Telefono = cita.Telefono;
                citaDb.Email = cita.Email;
                citaDb.Servicio = cita.Servicio;
                citaDb.Estado = cita.Estado;

                db.SaveChanges();
            }

            return RedirectToAction("RegistroCita");
        }

        // GET
        public IActionResult RegistroEgreso()
        {
            //using (var db = new TallerLaUnionContext())
            //{
            //    var citas = db.Citas.ToList();
            //    return View(citas);
            //}
            return View();
        }

        // POST - CREAR
        [HttpPost]
        public IActionResult RegistroEgreso(Cita cita)
        {
            //using (var db = new TallerLaUnionContext())
            //{
            //    db.Citas.Add(cita);
            //    db.SaveChanges();
            //}

            //return RedirectToAction("RegistroCita");
            return View();
        }
    }
}
*/
