using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppMinhaBahia.Models;

namespace AppMinhaBahia.Data
{
    public class CidadeRepositorio
    {
        private readonly AppMinhaBahiaContext _context;

        public CidadeRepositorio(AppMinhaBahiaContext context)
        {
            _context = context;
        }

        public List<Cidade> ListaDeCidades()
        {
            return _context.Cidades.ToList();
        }
    }
}
