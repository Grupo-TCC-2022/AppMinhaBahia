using System.Collections.Generic;

namespace AppMinhaBahia.Models
{
    public class Prefeitura
    {
        public int Id { get; set; }
        public string NomeCidade { get; set; }
        public IEnumerable<Setor> Setores { get; set; }
        public IEnumerable<Ocorrencia> Ocorrencias { get; set; }
        public IEnumerable<Requisicao> Requisicoes { get; set; }
    }
}
