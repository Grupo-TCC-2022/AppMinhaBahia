using Microsoft.AspNetCore.Mvc;
using AppMinhaBahia.Data;
using System;

namespace AppMinhaBahia.Controllers
{
    public class OcorrenciaController : Controller
    {
        private readonly OcorrenciaRepositorio _repositorio;

        public OcorrenciaController(OcorrenciaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public IActionResult Index(string status)
        {
            int usuarioId = Int32.Parse(User.FindFirst("id").Value);
            return View(_repositorio.ListaDeOcorrencias(usuarioId, status));
        }
    }
}
