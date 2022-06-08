using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AppMinhaBahia.Data;
using AppMinhaBahia.Models;
using Microsoft.AspNetCore.Authentication;
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

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar([Bind("NomeCompleto, CPF, NomeCidade, Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var usuarioExiste = _repositorio.UsuarioExiste(usuario.CPF);

                if (usuarioExiste)
                {
                    ModelState.AddModelError("CPF", "CPF já cadastrado no sistema");
                    return View(usuario);
                }

                await _repositorio.Cadastrar(usuario);
                return RedirectToAction("Index", "Home");
            }
            return View(usuario);
        }

        public IActionResult FazerLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FazerLogin(string CPF, string Senha)
        {
            var usuario = _repositorio.BuscarUsuarioPorCPF(CPF);
            
            if (usuario.Senha != Senha)
            {
                return View(usuario);
            }

            List<Claim> direitosAcesso = new List<Claim>{
                new Claim("id", usuario.Id.ToString())
            };

            var identidade = new ClaimsIdentity(direitosAcesso, "Identity.Login");
            var usuarioPrincipal = new ClaimsPrincipal(new[] { identidade });

            await HttpContext.SignInAsync(usuarioPrincipal, new AuthenticationProperties
            {
                IsPersistent = false,
                ExpiresUtc = DateTime.Now.AddHours(1)
            });
            
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReportarOcorrencia([Bind("Tipo, Descricao, LocalEspecifico")] Ocorrencia ocorrencia)
        {
            if (ModelState.IsValid)
            {
                int usuarioId = Int32.Parse(User.FindFirst("id").Value);
                await _repositorio.ReportarOcorrencia(usuarioId, ocorrencia);
                return RedirectToAction("Index", "Home");
            }
            return View(ocorrencia);
        }
    }
}