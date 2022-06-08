using System.Collections.Generic;
using System.Linq;

namespace AppMinhaBahia.Models
{
    public class Setor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Fundos { get; set; }
        public int PrefeituraId { get; set; }
        public Prefeitura Prefeitura { get; set; }
        public List<Funcionario> MaoDeObra { get; set; }
        public ICollection<Ocorrencia> Ocorrencias { get; set; }
        public IEnumerable<Requisicao> Requisicoes { get; set; }

        public string AbrirIntervencao(int ocorrenciaId)
        {
            var ocorrencia = this.Ocorrencias.FirstOrDefault(o => o.Id == ocorrenciaId);

            if (ocorrencia.Custo > this.Fundos)
            {
                return "Custo de ocorrencia excede os fundos do setor.";
            }

            int maoDeObraDisponivel = 0;
            for (int i = 0; i < MaoDeObra.Count; i++)
            {
                if (MaoDeObra[i].Disponivel)
                {
                    maoDeObraDisponivel += 1;
                }
            }
            if (ocorrencia.MaoDeObra > maoDeObraDisponivel)
            {
                return "Mão de obra necessaria excede a quantidade atual disponível do setor.";
            }

            if (ocorrencia.Status == "Aprovada")
            {
                return "Não pode aprovar uma ocorrência que já foi aprovada.";
            }

            ocorrencia.Status = "Aprovada";
            return "Ocorrência Aprovada com sucesso.";
        }
    }
}
