using System.Collections.Generic;
using System.Linq;

namespace AppMinhaBahia.Models
{
    public class UFAdmin : Usuario
    {
        public ICollection<Prefeitura> Prefeituras { get; set; }
        public ICollection<Requisicao> Requisicoes { get; set; }
        public double VerbaEstadual { get; set; }

        public string ProcessarRequisicao(int requisicaoId)
        {
            var requisicao = this.Requisicoes.FirstOrDefault(r => r.Id == requisicaoId);
            requisicao.Status = "Aprovada";
            return "Requisição aprovada";
        }
    }
}
