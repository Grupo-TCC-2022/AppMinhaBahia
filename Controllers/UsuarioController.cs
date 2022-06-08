using System.Threading.Tasks;
using AppMinhaBahia.Data;
using AppMinhaBahia.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppMinhaBahia.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepositorio _repositorio;

        public UsuarioController(UsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar([Bind("NomeCompleto, CPF, Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                await _repositorio.Criar(usuario);
                return RedirectToAction("Index", "Home");
            }
            return View(usuario);
        }
    }
}