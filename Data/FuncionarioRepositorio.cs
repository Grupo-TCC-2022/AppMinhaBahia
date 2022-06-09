using System.Linq;
using System.Threading.Tasks;
using AppMinhaBahia.Models;
using Microsoft.EntityFrameworkCore;

namespace AppMinhaBahia.Data
{
    public class FuncionarioRepositorio
    {
        private readonly AppMinhaBahiaContext _context;

        public FuncionarioRepositorio(AppMinhaBahiaContext context)
        {
            _context = context;
        }

        public Funcionario BuscarFuncionarioPorId(int id)
        {
            var funcionario = _context.Funcionarios
            .Include(f => f.Cidade)
            .Include(f => f.Setor)
            .Include(f => f.Intervencao)
            .FirstOrDefault(f => f.Id == id);
            return funcionario;
        }
    }
}
