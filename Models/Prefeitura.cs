using System;
using System.Collections.Generic;
using System.Linq;

namespace AppMinhaBahia.Models
{
    public class Prefeitura : Usuario
    {
        public UFAdmin UF { get; set; }
        public ICollection<Setor> Setores { get; set; }
        public ICollection<Requisicao> Requisicoes { get; set; }
        public double VerbaMunicipal { get; set; }
        public double SalarioMedioPorFuncionario { get; set; }

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

        public string ProcessarRequisicao(int requisicaoId)
        {
            var requisicao = this.Requisicoes.FirstOrDefault(r => r.Id == requisicaoId);

            if (requisicao.Tipo == "Verba")
            {
                if (requisicao.Verba > this.VerbaMunicipal)
                {
                    return "Verba solicitada excede o recurso estadual.";
                }

                requisicao.Status = "Aprovada";
                return "Requisição aprovada.";
            }

            if (requisicao.Tipo == "Contratação")
            {
                double custoTotalContratacao = (double) requisicao.Funcionarios * this.SalarioMedioPorFuncionario;

                if (custoTotalContratacao > this.VerbaMunicipal)
                {
                    return "Contratação solicitada excede o recurso estadual.";
                }

                requisicao.Status = "Reprovada";
                return "Requisição reprovada.";
            }

            return "Tipo de requisição não informado.";
        }

        public Requisicao SolicitarVerbaEstadual(double valor)
        {
            Requisicao requisicao = new Requisicao();
            requisicao.Tipo = "Verba";
            requisicao.Prefeitura = this;
            requisicao.Data = DateTime.Today;
            requisicao.Verba = valor;

            this.UF.Requisicoes.Add(requisicao);
            return requisicao;
        }
    }
}
