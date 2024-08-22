using Estancia;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace MVC.Controllers
{
    public class TareasController : Controller
    {
        Sistema _sistema = Sistema.Instance;

        public IActionResult Editar(int id)
        {
            if (HttpContext.Session.GetString("rol") == "peon")
            {
                try
                {
                    //Busco peon por la session y luego su tarea segun el id.
                    Peon peon = _sistema.BuscarPeon(HttpContext.Session.GetString("email"));
                    TareaAsignada tareaA = peon.BuscarTareaAsignada(id);
                    return View(tareaA);
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
                return View();
            }
            return RedirectToAction("Index", "home");
        }

        [HttpPost]
        public IActionResult Editar(TareaAsignada tareaA)
        {
            if (HttpContext.Session.GetString("rol") == "peon")
            {
                try
                {
                    //Busco peon por la session y luego su tarea segun el id.
                    Peon peon = _sistema.BuscarPeon(HttpContext.Session.GetString("email"));
                    peon.actualizarTareaAsignada(tareaA);
                    TempData["exitoEditarTareaAsignada"] = $"La tarea {tareaA.Comentario} fue completada y cerrada";

                    //redirecciono a tareas no completadas del peon
                    return RedirectToAction("TareasNoCompletadas", "Empleados");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
                return View(tareaA);
            }
            return RedirectToAction("Index", "home");
        }

        

        public IActionResult Alta()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Alta(string descripcion)
        {
            try
            {
                Tarea tareaNueva = new Tarea(descripcion);
                _sistema.InstanceTask(tareaNueva);
                TempData["Exito"] = "Tarea agregada correctamente.";
                return RedirectToAction("SeleccionarPeon", "Empleados");
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }
       
        
    }
}
