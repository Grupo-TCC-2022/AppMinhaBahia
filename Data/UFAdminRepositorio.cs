using System.Linq;
using System.Threading.Tasks;
using AppMinhaBahia.Models;
using Microsoft.EntityFrameworkCore;

namespace AppMinhaBahia.Data
{
    public class UFAdminRepositorio
    {
        private readonly AppMinhaBahiaContext _context;

        public UFAdminRepositorio(AppMinhaBahiaContext context)
        {
            _context = context;
        }

        public UFAdmin BuscarUFAdminPorId(int id)
        {
            var admin = _context.Administradores
            .Include(a => a.Prefeituras)
            .ThenInclude(p => p.Cidade)
            .Include(a => a.Requisicoes)
            .ThenInclude(r => r.Prefeitura)
            .ThenInclude(p => p.Cidade)
            .Include(a => a.Requisicoes)
            .ThenInclude(r => r.Setor)
            .ThenInclude(s => s.Cidade)
            .FirstOrDefault(a => a.Id == id);
            return admin;
        }
    }
}
