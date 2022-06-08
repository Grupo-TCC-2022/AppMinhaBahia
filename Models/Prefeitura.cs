using System.Collections.Generic;
using System.Linq;

namespace AppMinhaBahia.Models
{
    public class Prefeitura
    {
        public int Id { get; set; }
        public string NomeCidade { get; set; }
        public ICollection<Setor> Setores { get; set; }
        public ICollection<Ocorrencia> Ocorrencias { get; set; }
        public IEnumerable<Requisicao> Requisicoes { get; set; }

        public void DefinirCusto(int ocorrenciaId, double valor)
        {
            var ocorrencia = this.Ocorrencias.FirstOrDefault(o => o.Id == ocorrenciaId);
            ocorrencia.Custo = valor;
        }

        public void DefinirMaoDeObra(int ocorrenciaId, int numeroDeFuncionarios)
        {
            var ocorrencia = this.Ocorrencias.FirstOrDefault(o => o.Id == ocorrenciaId);
            ocorrencia.MaoDeObra = numeroDeFuncionarios;
        }

        public void  EncaminharOcorrencia(int ocorrenciaId, int setorId)
        {
            var ocorrencia = this.Ocorrencias.FirstOrDefault(o => o.Id == ocorrenciaId);
            var setor = this.Setores.FirstOrDefault(s => s.Id == setorId);
            ocorrencia.Status = "Encaminhada";
            setor.Ocorrencias.Add(ocorrencia);
        }
    }
}
