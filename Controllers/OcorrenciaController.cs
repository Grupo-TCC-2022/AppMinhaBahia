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

        public OcorrenciaController(OcorrenciaRepositorio repositorio, UsuarioRepositorio usuarioRepositorio)
        {
            _repositorio = repositorio;
            _usuarioRepositorio = usuarioRepositorio;
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
                var prefeitura = _usuarioRepositorio.BuscarPrefeituraPorCidade(usuario.Cidade.Nome);
                ocorrencia.Prefeitura = prefeitura;

                await _repositorio.Criar(ocorrencia);
                return RedirectToAction("Index");
            }
            return View(ocorrencia);
        }
    }
}
