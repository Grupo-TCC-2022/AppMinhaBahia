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
    public class PrefeituraController : Controller
    {
        private readonly PrefeituraRepositorio _repositorio;

        public PrefeituraController(PrefeituraRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        /* TODO: Carregar todas as prefeituras para a UF */
        /* ANTES: Implementar o repositorio de prefeitura */
        public IActionResult Index()
        {
            return View();
        }

        /* TODO: Criar nova prefeitura para a UF */
        /* ANTES: Implemente o repositorio da prefeitura  */
        [HttpPost]
        public async Task<IActionResult> Criar([Bind("..., ..., ...")] Prefeitura prefeitura)
        {
            return View();
        }

        public IActionResult Criar()
        {
            return View();
        }
    }
}