using Microsoft.AspNetCore.Mvc;
using AppMinhaBahia.Data;
using System;
using System.Threading.Tasks;
using AppMinhaBahia.Models;

namespace AppMinhaBahia.Controllers
{
    public class OcorrenciaController : Controller
    {
        private readonly OcorrenciaRepositorio _repositorio;
        private readonly UsuarioRepositorio _usuarioRepositorio;
        private readonly PrefeituraRepositorio _prefeituraRepositorio;

        public OcorrenciaController(OcorrenciaRepositorio repositorio, UsuarioRepositorio usuarioRepositorio, PrefeituraRepositorio prefeituraRepositorio)
        {
            _repositorio = repositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _prefeituraRepositorio = prefeituraRepositorio;
        }

        public IActionResult Index(string status)
        {
            return View(_repositorio.ColecaoDeOcorrencias(status));
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar([Bind("Tipo, Descricao, LocalEspecifico")] Ocorrencia ocorrencia)
        {
            if (ModelState.IsValid)
            {
                int usuarioId = Int32.Parse(User.FindFirst("id").Value);
                var usuario = _usuarioRepositorio.BuscarUsuarioPorId(usuarioId);
                ocorrencia.Usuario = usuario;
                var prefeitura = _prefeituraRepositorio.BuscarPrefeituraPorCidade(usuario.Cidade.Nome);
                ocorrencia.Prefeitura = prefeitura;

                await _repositorio.Criar(ocorrencia);
                return RedirectToAction("Index", "Home");
            }
            return View(ocorrencia);
        }
    }
}
