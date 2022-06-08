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

        public async Task Criar(Usuario usuario)
        {
            _context.Add(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
