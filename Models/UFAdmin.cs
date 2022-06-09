using System;
using System.Collections.Generic;
using System.Linq;

namespace AppMinhaBahia.Models
{
    public class UFAdmin : Usuario
    {
        public string NomeUF { get; set; }
        public ICollection<Prefeitura> Prefeituras { get; set; }
        public ICollection<Requisicao> Requisicoes { get; set; }
        public double VerbaEstadual { get; set; } = Double.MaxValue;

        public Dictionary<string, string> ProcessarRequisicao(int requisicaoId)
        {
            var requisicao = this.Requisicoes.FirstOrDefault(r => r.Id == requisicaoId);
            Dictionary<string, string> situacao = new Dictionary<string,string>();

            if (requisicao.Verba > this.VerbaEstadual)
            {
                situacao.Add("status", "erro");
                situacao.Add("mensagem", "O Estado faliu");
                return situacao;
            }

            requisicao.Status = "Aprovada";
            this.VerbaEstadual -= (double) requisicao.Verba;
            requisicao.Prefeitura.VerbaMunicipal += (double) requisicao.Verba;
            situacao.Add("status", "sucesso");
            situacao.Add("mensagem", "Requisição aprovada");
            return situacao;
        }
    }
}
