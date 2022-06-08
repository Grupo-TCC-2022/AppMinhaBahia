using System.Collections.Generic;
using System.Linq;
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

        public Cidade BuscarCidadePorNome(string nome)
        {
            var cidade = _context.Cidades.FirstOrDefault(c => c.Nome == nome);
            return cidade;
        }

        public List<Cidade> ListaDeCidades()
        {
            return _context.Cidades.ToList();
        }
    }
}
