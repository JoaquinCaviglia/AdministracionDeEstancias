using Estancia;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class EmpleadosController : Controller
    {
        Sistema _sistema = Sistema.Instance;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RegistrarPeon()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarPeon(Peon nuevoPeon)
        {
            try
            {
                _sistema.InstanceEmpleados(nuevoPeon);
                TempData["Mensaje"] = $"{nuevoPeon.Nombre} Registrado Correctamente!";
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        public IActionResult PerfilPeon()
        {
            if (HttpContext.Session.GetString("rol") == "peon")
            {
                try
                {
                    return View(_sistema.BuscarPeon(HttpContext.Session.GetString("email")));
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }

                return View();
            }
            return RedirectToAction("Index", "home");
        }

        //ASK: tengo alguna manera más facil de acceder al peon para luego filtrar sus tareas asignadas no completadas? En este caso saco el dato de la session
        public IActionResult TareasNoCompletadas()
        {
            if (HttpContext.Session.GetString("rol") == "peon")
            {
                List<TareaAsignada> tareasNoCompletadas = new List<TareaAsignada>();

                ViewBag.Mensaje = TempData["exitoEditarTareaAsignada"];

                try
                {
                    Peon peon = _sistema.BuscarPeon(HttpContext.Session.GetString("email"));
                    foreach (TareaAsignada unaTa in peon.TareasAsignadas)
                    {
                        if (unaTa.Comentario == null)
                            tareasNoCompletadas.Add(unaTa);
                    }
                    tareasNoCompletadas.Sort();
                    return View(tareasNoCompletadas);
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
                return View();
            }
            return RedirectToAction("Index", "home");
        }

        public IActionResult VerPeones()
        {
            if (HttpContext.Session.GetString("rol") == "capataz")
            {
                List<Peon> peones = new List<Peon>();

                try
                {
                    peones = _sistema.GetPeones();
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
                return View(peones);
            }
            return RedirectToAction("Index", "home");
        }

        public IActionResult ListarParaAsignar(string email)
        {
            if (HttpContext.Session.GetString("rol") == "capataz")
            {
                if (TempData["Email"] != null)
                    ViewBag.Email = TempData["Email"];
                else
                {
                    ViewBag.Email = email;
                }

                ViewBag.Exito = TempData["Exito"];
                return View(_sistema.GetTareas());
            }
            return RedirectToAction("Index", "home");
        }


        public IActionResult Asignar(int id, string email)
        {
            if (HttpContext.Session.GetString("rol") == "capataz")
            {
                ViewBag.Email = TempData["email"];
                try
                {
                    DateTime fechaHoy = new DateTime();
                    Peon peon = _sistema.BuscarPeon(email);
                    Tarea tarea = _sistema.BuscarTarea(id);
                    TareaAsignada tareaAsignada = new TareaAsignada(fechaHoy, tarea);
                    peon.TareasAsignadas.Add(tareaAsignada);
                    TempData["Exito"] = "Tarea asignada correctamente";
                    TempData["Email"] = email;
                    return RedirectToAction("ListarParaAsignar");

                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
                return RedirectToAction("SeleccionarPeon");
            }
            return RedirectToAction("Index", "home");
        }
        public IActionResult VerTareas(string email)
        {
            if (HttpContext.Session.GetString("rol") == "capataz")
            {
                List<TareaAsignada> tareasPeon = new List<TareaAsignada>();
                try
                {
                    Peon peon = _sistema.BuscarPeon(email);
                    foreach (TareaAsignada unTa in peon.TareasAsignadas)
                        tareasPeon.Add(unTa);
                    return View(tareasPeon);
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
                return View();
            }
            return RedirectToAction("Index", "home");
        }
        public IActionResult SeleccionarPeon()
        {
            if (HttpContext.Session.GetString("rol") == "capataz")
            {
                List<Peon> peones = new List<Peon>();
                ViewBag.Error = TempData["Error"];
                ViewBag.Exito = TempData["Exito"];
                try
                {
                    peones = _sistema.GetPeones();
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
                return View(peones);
            }
            return RedirectToAction("Index", "home");
        }

    }
}
