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

        public async Task Cadastrar(Usuario usuario)
        {
            _context.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public Usuario BuscarUsuarioPorCPF(string CPF)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.CPF == CPF);
            return usuario;
        }

        public bool UsuarioExiste(string CPF)
        {
            var existe = _context.Usuarios.Any(u => u.CPF == CPF);
            return existe;
        }

        public async Task ReportarOcorrencia(int usuarioId, Ocorrencia ocorrencia)
        {
            var usuarioAtual = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);
            var prefeitura = _context.Prefeituras.FirstOrDefault(p => p.Cidade == usuarioAtual.Cidade);

            usuarioAtual.ReportarOcorrencia(ocorrencia, prefeitura);
            await _context.SaveChangesAsync();
        }
    }
}
