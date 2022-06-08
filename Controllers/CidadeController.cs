using Microsoft.AspNetCore.Mvc;
using AppMinhaBahia.Data;
using System.Linq;

namespace AppMinhaBahia.Controllers
{
    public class CidadeController : Controller
    {
        private readonly AppMinhaBahiaContext _context;

        public CidadeController(AppMinhaBahiaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetCidades()
        {
            return Json(_context.Cidades.ToList());
        }
    }
}
