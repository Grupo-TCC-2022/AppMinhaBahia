using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AppMinhaBahia.Models
{
    public class Prefeitura : Usuario
    {
        public UFAdmin UF { get; set; }
        public ICollection<Setor> Setores { get; set; }
        public ICollection<Requisicao> Requisicoes { get; set; }
        [Display(Name = "Verba municipal")]
        public double VerbaMunicipal { get; set; } = 0;
        [Display(Name = "Salario médio por funcionário")]
        public double SalarioMedioPorFuncionario { get; set; } = 1.212; /* Salario mínimo */


        //Definir custo da ocorrencia
        public void DefinirCusto(int ocorrenciaId, double valor)
        {
            var ocorrencia = this.Ocorrencias.FirstOrDefault(o => o.Id == ocorrenciaId);
            ocorrencia.Custo = valor;
        }
        //Define numero necessario de funcionário
        public void DefinirFuncionarios(int ocorrenciaId, int numeroDeFuncionarios)
        {
            var ocorrencia = this.Ocorrencias.FirstOrDefault(o => o.Id == ocorrenciaId);
            ocorrencia.Funcionarios = numeroDeFuncionarios;
        }
        //Encaminha ocorrencia
        public Dictionary<string, string> EncaminharOcorrencia(int ocorrenciaId, int setorId)
        {
            var ocorrencia = this.Ocorrencias.FirstOrDefault(o => o.Id == ocorrenciaId);
            Dictionary<string, string> situacao = new Dictionary<string,string>();
            
            if (ocorrencia.Custo == null)
            {   
                situacao.Add("status", "alerta");
                situacao.Add("mensagem", "Custo de ocorrencia ainda não processado");
                return situacao;
            }
            if (ocorrencia.Funcionarios == null)
            {
                situacao.Add("status", "alerta");
                situacao.Add("mensagem", "Mão de obra necessaria ainda não processada");
                return situacao;
            }

            var setor = this.Setores.FirstOrDefault(s => s.Id == setorId);
            
            if (ocorrencia.Status == "Arquivada")
            {
                situacao.Add("status", "erro");
                situacao.Add("mensagem", "Não pode encaminhar uma ocorrência arquivada");
                return situacao;
            }
            if (ocorrencia.Status == "Encaminhada")
            {
                situacao.Add("status", "erro");
                situacao.Add("mensagem", "Não pode encaminhar uma ocorrência que já foi encaminhada");
                return situacao;
            }

            ocorrencia.Status = "Encaminhada";
            setor.Ocorrencias.Add(ocorrencia);
            situacao.Add("status", "sucesso");
            situacao.Add("mensagem", "Ocorrência encaminhada com sucesso");
            return situacao;
        }

        public Dictionary<string, string> ArquivarOcorrencia(int ocorrenciaId)
        {
            Dictionary<string, string> situacao = new Dictionary<string,string>();
            var ocorrencia = this.Ocorrencias.FirstOrDefault(o => o.Id == ocorrenciaId);
            ocorrencia.Status = "Arquivada";
            situacao.Add("status", "sucesso");
            situacao.Add("mensagem", "Ocorrência arquivada com sucesso");
            return situacao;
        }

        public Dictionary<string, string> ProcessarRequisicao(int requisicaoId)
        {
            var requisicao = this.Requisicoes.FirstOrDefault(r => r.Id == requisicaoId);
            Dictionary<string, string> situacao = new Dictionary<string,string>();

            if (requisicao.Tipo == "Verba")
            {
                if (requisicao.Verba > this.VerbaMunicipal)
                {
                    situacao.Add("status", "erro");
                    situacao.Add("mensagem", "Verba solicitada excede o recurso municipal");
                    return situacao;
                }

                requisicao.Status = "Aprovada";
                requisicao.Setor.Verba += (double) requisicao.Verba;
                this.VerbaMunicipal -= (double)requisicao.Verba;
                situacao.Add("status", "sucesso");
                situacao.Add("mensagem", "Requisição aprovada");
                return situacao;
            }

            if (requisicao.Tipo == "Contratação")
            {
                double custoTotalContratacao = (double) requisicao.Funcionarios * this.SalarioMedioPorFuncionario;

                if (custoTotalContratacao > this.VerbaMunicipal)
                {
                    situacao.Add("status", "erro");
                    situacao.Add("mensagem", "Contratação solicitada excede o recurso municipal");
                    return situacao;
                }
                
  
                // Forma inadequada de implementar, so existe para não fugirmos do escopo simplex desse projeto
                Random random = new Random();
                for (int i = 0; i < requisicao.Funcionarios; i++)
                {
                    requisicao.Setor.Funcionarios.Add(new Funcionario() {
                        CPF = random.Next().ToString(),
                        NomeCompleto = random.Next().ToString(),
                        Cidade = this.Cidade
                    });
                }
                situacao.Add("status", "sucesso");
                situacao.Add("mensagem", "Requisição aprovada");
                return situacao;
            }

            situacao.Add("status", "alerta");
            situacao.Add("mensagem", "Tipo de requisição não informado");
            return situacao;
        }

        public void SolicitarVerbaEstadual(double valor)
        {
            Requisicao requisicao = new Requisicao();
            requisicao.Tipo = "Verba";
            requisicao.Prefeitura = this;
            requisicao.Data = DateTime.Today;
            requisicao.Verba = valor;

            this.UF.Requisicoes.Add(requisicao);
        }
    }
}
