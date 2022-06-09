using System.Linq;
using System.Threading.Tasks;
using AppMinhaBahia.Models;
using Microsoft.EntityFrameworkCore;

namespace AppMinhaBahia.Data
{
    public class PrefeituraRepositorio
    {
        private readonly AppMinhaBahiaContext _context;

        public PrefeituraRepositorio(AppMinhaBahiaContext context)
        {
            _context = context;
        }

        public Prefeitura BuscarPrefeituraPorCidade(string cidade)
        {
            var prefeitura = _context.Prefeituras.FirstOrDefault(p => p.Cidade.Nome == cidade);
            return prefeitura;
        }
    }
}
