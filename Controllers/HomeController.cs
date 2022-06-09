using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AppMinhaBahia.Models;
using AppMinhaBahia.Data;
using System;

namespace AppMinhaBahia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UsuarioRepositorio _usuarioRepositorio;
        private readonly PrefeituraRepositorio _prefeituraRepositorio;
        private readonly UFAdminRepositorio _adminRepositorio;
        private readonly FuncionarioRepositorio _funcionarioRepositorio;

        public HomeController(ILogger<HomeController> logger, UsuarioRepositorio usuarioRepositorio, PrefeituraRepositorio prefeituraRepositorio, UFAdminRepositorio adminRepositorio, FuncionarioRepositorio funcionarioRepositorio)
        {
            _logger = logger;
            _prefeituraRepositorio = prefeituraRepositorio;
            _adminRepositorio = adminRepositorio;
            _funcionarioRepositorio = funcionarioRepositorio;
        }

        public IActionResult Index()
        {
            string tipoUsuario = User.FindFirst("tipo").Value;
            int usuarioId = Int32.Parse(User.FindFirst("id").Value);
            Usuario usuario =  new Usuario();

            if (tipoUsuario == "comum")
            {
                usuario = _usuarioRepositorio.BuscarUsuarioPorId(usuarioId);
            }
            else if (tipoUsuario == "prefeitura")
            {
                usuario = _prefeituraRepositorio.BuscarPrefeituraPorId(usuarioId);
            } 
            else if (tipoUsuario == "admin")
            {
                usuario = _adminRepositorio.BuscarUFAdminPorId(usuarioId);
            }
            else if (tipoUsuario == "funcionario")
            {
                usuario = _funcionarioRepositorio.BuscarFuncionarioPorId(usuarioId);
            }

            return View(usuario);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
