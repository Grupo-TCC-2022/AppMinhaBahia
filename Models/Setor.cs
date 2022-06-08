using System.Collections.Generic;

namespace AppMinhaBahia.Models
{
    public class Setor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Fundos { get; set; }
        public int PrefeituraId { get; set; }
        public Prefeitura Prefeitura { get; set; }
        public IEnumerable<Funcionario> MaoDeObra { get; set; }
        public ICollection<Ocorrencia> Ocorrencias { get; set; }
        public IEnumerable<Requisicao> Requisicoes { get; set; }
    }
}
