using System.Collections.Generic;
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

        /* TODO: Carregar todas as prefeituras para a UF */
        public IEnumerable<Prefeitura> CarregarPrefeituras(int UFId)
        {
            return new List<Prefeitura>();
        }

        /* TODO: Criar nova prefeitura para a UF */
        public async Task Criar(Prefeitura prefeitura)
        {
        }

        public Prefeitura BuscarPrefeituraPorId(int id)
        {
            var prefeitura = _context.Prefeituras
            .Include(p => p.Setores)
            .Include(p => p.Requisicoes)
            .FirstOrDefault(p => p.Id == id);
            return prefeitura;
        }

        public Prefeitura BuscarPrefeituraPorCidade(string cidade)
        {
            var prefeitura = _context.Prefeituras.FirstOrDefault(p => p.Cidade.Nome == cidade);
            return prefeitura;
        }
    }
}
