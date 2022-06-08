using System.Collections.Generic;
using System.Linq;
using AppMinhaBahia.Models;

namespace AppMinhaBahia.Data
{
    public class OcorrenciaRepositorio
    {
        private readonly AppMinhaBahiaContext _context;

        public OcorrenciaRepositorio(AppMinhaBahiaContext context)
        {
            _context = context;
        }

        public IEnumerable<Ocorrencia> ListaDeOcorrencias(int usuarioId, string status)
        {
            return _context.Ocorrencias.Where(
                o => o.UsuarioId == usuarioId
                &&
                o.Status == status
            ).ToList();
        }
    }
}
