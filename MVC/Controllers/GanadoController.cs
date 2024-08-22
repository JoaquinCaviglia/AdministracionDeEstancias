using Estancia;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class GanadoController : Controller
    {
        Sistema _miSistema = Sistema.Instance;
        public IActionResult Index()
        {

            if (HttpContext.Session.GetString("rol") == "peon")
            {
                ViewBag.Ganado = _miSistema.GetAnimales();
                ViewBag.Mensaje = TempData["exitoAltaBovino"];
                ViewBag.Vacunado = TempData["exitoInyeccion"];
                ViewBag.Error = TempData["errorInyeccion"];
                return View();
            }

            if (HttpContext.Session.GetString("rol") == "capataz")
            {
                ViewBag.Ganado = _miSistema.GetAnimales();
                ViewBag.Mensaje = TempData["exitoAltaBovino"];
                return View();
            }

                return RedirectToAction("Index", "home");
        }

        public IActionResult VacunarAnimal(string id)
        {
            if (HttpContext.Session.GetString("rol") == "capataz" || HttpContext.Session.GetString("rol") == "peon")
            {
                ViewBag.Vacunas = _miSistema.GetVacunas();
                return View(_miSistema.GetUnAnimal(id));
            }
            return RedirectToAction("Index", "home");
        }

        [HttpPost]
        public IActionResult VacunarAnimal(string idAnimal, string nombreVacuna, DateTime fechaIny)
        {
            if (HttpContext.Session.GetString("rol") == "peon")
            {
                ViewBag.Vacunas = _miSistema.GetVacunas();

                try
                {
                    Res animalAVacunar = _miSistema.GetUnAnimal(idAnimal);
                    animalAVacunar.EsMayorATresMeses();
                    Vacuna vacuna = _miSistema.GetUnaVacuna(nombreVacuna);
                    DateTime hoy = fechaIny;
                    Inyeccion nuevaInyeccion = new Inyeccion(vacuna, hoy, hoy.AddYears(1));
                    animalAVacunar.Inyecciones.Add(nuevaInyeccion);
                    TempData["exitoInyeccion"] = $"Se inyectó el animal {animalAVacunar.Id} con la vacuna {vacuna.Nombre} correctamente";

                }
                catch (Exception ex)
                {
                    TempData["errorInyeccion"] = ex.Message;
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "home");
        }

        public IActionResult AltaBovino()
        {
            if (HttpContext.Session.GetString("rol") == "capataz")
            {
                return View();
            }
            return RedirectToAction("Index", "home");
        }

        [HttpPost]
        public IActionResult AltaBovino(Bovino nuevoBovino)
        {
            if (HttpContext.Session.GetString("rol") == "capataz")
            {
                try
                {
                    _miSistema.InstanceRes(nuevoBovino);
                    TempData["exitoAltaBovino"] = $"Se dio de alta el bovino {nuevoBovino.Id}";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
                return View(nuevoBovino);
            }
            return RedirectToAction("Index", "home");
        }

        public IActionResult ResesLibresParaAsignar()
        {
            if (HttpContext.Session.GetString("rol") == "capataz")
            {
                List<Res> reses = _miSistema.GetAnimales();
                List<Res> resesLibres = new List<Res>();
                foreach (Res res in reses)
                {
                    if (res.Libre)
                        resesLibres.Add(res);
                }
                return View(resesLibres);
            }
            return RedirectToAction("Index", "home");
        }

        public IActionResult AnimalesPorPesoYTipo()
        {
            return View(_miSistema.GetAnimales());
        }

        [HttpPost]
        public IActionResult AnimalesPorPesoYTipo(int peso, string tipo)
        {
            if (HttpContext.Session.GetString("rol") == "capataz")
            {
                List<Res> listado = new List<Res>();
                List<Res> resesPorTipo = _miSistema.GetResesPorTipo(tipo);

                foreach (Res unaR in resesPorTipo)
                    if (unaR.PesoActual > peso)
                        listado.Add(unaR);

                return View(listado);
            }
            return RedirectToAction("Index", "home");
        }
    }
}
