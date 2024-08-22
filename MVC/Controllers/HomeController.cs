using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Diagnostics;
using Estancia;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private Sistema _sistema = Sistema.Instance;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            try
            {
                Empleado usuario = _sistema.AutenticarUsuario(email, password);
                HttpContext.Session.SetString("nombre", usuario.Nombre);
                HttpContext.Session.SetString("rol", usuario.Rol());
                HttpContext.Session.SetString("email", usuario.Email);
                return RedirectToAction("index", "home");

            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}

