using Estancia;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class PotrerosController : Controller
    {
        Sistema _miSistema = Sistema.Instance;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListarPotreros(string id)
        {
            if (HttpContext.Session.GetString("rol") == "capataz")
            {
                ViewBag.IdRes = id;
                List<Potrero> potreros = _miSistema.GetPotreros();
                List<Potrero> potrerosDisponibles = new List<Potrero>();
                foreach (Potrero unP in potreros)
                {
                    if (unP.HayEspacio())
                    {
                        potrerosDisponibles.Add(unP);
                    }
                }
                return View(potrerosDisponibles);
            }
            return RedirectToAction("Index", "home");
        }
        public IActionResult AgregarRes(int IdPotrero, string IdRes)
        {
            if (HttpContext.Session.GetString("rol") == "capataz")
            {
                try
                {
                    Potrero potrero = _miSistema.BuscarPotrero(IdPotrero);
                    Res res = _miSistema.GetUnAnimal(IdRes);
                    potrero.AddRes(res);
                    res.Libre = false;
                    TempData["MensajeExito"] = $"El animal {res.Id} fué agregado correctamente";
                }
                catch (Exception ex)
                {
                    TempData["MensajeError"] = ex.Message;
                }
                return RedirectToAction("ResesLibresParaAsignar", "Ganado");
            }
            return RedirectToAction("Index", "home");
        }

        public IActionResult ListarTodosLosPotreros()
        {
            if (HttpContext.Session.GetString("rol") == "capataz")
            {
                List<Potrero> potreros = _miSistema.GetPotreros();
                try
                {
                    potreros.Sort();
                    return View(potreros);
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
                return View();
            }
            return RedirectToAction("Index", "home");
        }
    }
}
