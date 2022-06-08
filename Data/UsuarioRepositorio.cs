using System.Linq;
using System.Threading.Tasks;
using AppMinhaBahia.Models;

namespace AppMinhaBahia.Data
{
    public class UsuarioRepositorio
    {
        private readonly AppMinhaBahiaContext _context;

        public UsuarioRepositorio(AppMinhaBahiaContext context)
        {
            _context = context;
        }

        public async Task Registrar(Usuario usuario)
        {
            _context.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task ReportarOcorrencia(Ocorrencia ocorrencia)
        {
            // TODO:
            var prefeitura = _context.Prefeituras.FirstOrDefault(p => p.NomeCidade == USUARIOATUALCIDADE);
            USUARIOATUAL.ReportarOcorrencia(ocorrencia, Prefeitura);
            await _context.SaveChangesAsync();
        }
    }
}
