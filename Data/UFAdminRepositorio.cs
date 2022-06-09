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
            .FirstOrDefault(a => a.Id == id);
            return admin;
        }
    }
}
