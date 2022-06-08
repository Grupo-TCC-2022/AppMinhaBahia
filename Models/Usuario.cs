using System.Collections.Generic;

namespace AppMinhaBahia.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public IEnumerable<Ocorrencia> Ocorrencias { get; set; }
    }
}
