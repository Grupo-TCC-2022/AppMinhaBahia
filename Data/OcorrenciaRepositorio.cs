using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppMinhaBahia.Models;
using Microsoft.EntityFrameworkCore;

namespace AppMinhaBahia.Data
{
    public class OcorrenciaRepositorio
    {
        private readonly AppMinhaBahiaContext _context;

        public OcorrenciaRepositorio(AppMinhaBahiaContext context)
        {
            _context = context;
        }

        public async Task Criar(Ocorrencia ocorrencia)
        {
            _context.Add(ocorrencia);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Ocorrencia> ColecaoDeOcorrencias(string status)
        {
            
            return _context.Ocorrencias.Include(o => o.Intervencao).Include(o => o.Prefeitura).Include(o => o.Usuario).ThenInclude(u => u.Cidade).Where(o => o.Status == status).ToList();
        }
    }
}
