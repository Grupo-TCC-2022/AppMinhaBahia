using Microsoft.AspNetCore.Mvc;
using AppMinhaBahia.Data;

namespace AppMinhaBahia.Controllers
{
    public class CidadeController : Controller
    {
        private readonly CidadeRepositorio _repositorio;

        public CidadeController(CidadeRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public JsonResult GetCidades()
        {
            return Json(_repositorio.ListaDeCidades());
        }
    }
}
