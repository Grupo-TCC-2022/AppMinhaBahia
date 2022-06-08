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
        public async Task<IActionResult> Registrar([Bind("NomeCompleto, CPF, Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                await _repositorio.Registrar(usuario);
                return RedirectToAction("Index", "Home");
            }
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReportarOcorrencia([Bind("Tipo, Descricao, LocalEspecifico")] Ocorrencia ocorrencia)
        {
            if (ModelState.IsValid)
            {
                await _repositorio.ReportarOcorrencia(ocorrencia);
                return RedirectToAction("Index", "Home");
            }
            return View(ocorrencia);
        }
    }
}