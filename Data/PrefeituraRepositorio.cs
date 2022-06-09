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

        /* Carregar todas as prefeituras para a UF */
        public IEnumerable<Prefeitura> CarregarPrefeituras(int UFId)
        {
            return _context.Prefeituras.Include(p => p.UF).Include(p => p.Setores).Include(p => p.Requisicoes).ToList();
        }

        /* Criar nova prefeitura para a UF */
        public async Task Criar(Prefeitura prefeitura)
        {
            _context.Add(prefeitura);
            await _context.SaveChangesAsync();
        }

        /* Atualizar para carregar junto com a prefeitura suas associações */
        public Prefeitura BuscarPrefeituraPorId(int id)
        {
            var prefeitura = _context.Prefeituras
            .Include(p => p.Setores)
            .ThenInclude(p => p.Prefeitura).ThenInclude(p => p.Requisicoes)
            .Include(p => p.Requisicoes)
            .ThenInclude(p => p.Setor).ThenInclude(p => p.Prefeitura)
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
