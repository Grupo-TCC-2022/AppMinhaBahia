using System.Collections.Generic;
using System.Linq;

namespace AppMinhaBahia.Models
{
    public class Prefeitura : Usuario
    {
        public string NomeCidade { get; set; }
        public ICollection<Setor> Setores { get; set; }
        public IEnumerable<Requisicao> Requisicoes { get; set; }

        public void DefinirCusto(int ocorrenciaId, double valor)
        {
            var ocorrencia = this.Ocorrencias.FirstOrDefault(o => o.Id == ocorrenciaId);
            ocorrencia.Custo = valor;
        }

        public void DefinirFuncionarios(int ocorrenciaId, int numeroDeFuncionarios)
        {
            var ocorrencia = this.Ocorrencias.FirstOrDefault(o => o.Id == ocorrenciaId);
            ocorrencia.Funcionarios = numeroDeFuncionarios;
        }

        public string EncaminharOcorrencia(int ocorrenciaId, int setorId)
        {
            var ocorrencia = this.Ocorrencias.FirstOrDefault(o => o.Id == ocorrenciaId);

            if (ocorrencia.Custo == null)
            {
                return "Custo de ocorrencia ainda não processado.";
            }
            if (ocorrencia.Funcionarios == null)
            {
                return "Mão de obra necessaria ainda não processada.";
            }

            var setor = this.Setores.FirstOrDefault(s => s.Id == setorId);

            if (ocorrencia.Status == "Encaminhada")
            {
                return "Não pode encaminhar uma ocorrência que já foi encaminhada.";
            }

            ocorrencia.Status = "Encaminhada";
            setor.Ocorrencias.Add(ocorrencia);
            return "Ocorrência encaminhada com sucesso.";
        }

        public string ArquivarOcorrencia(int ocorrenciaId)
        {
            var ocorrencia = this.Ocorrencias.FirstOrDefault(o => o.Id == ocorrenciaId);
            ocorrencia.Status = "Arquivada";
            return "Ocorrência arquivada com sucesso.";
        }
    }
}
