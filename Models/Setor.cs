using System;
using System.Collections.Generic;
using System.Linq;

namespace AppMinhaBahia.Models
{
    public class Setor : Usuario
    {
        public string NomeSetor { get; set; }
        public double Verba { get; set; }
        public int PrefeituraId { get; set; }
        public Prefeitura Prefeitura { get; set; }
        public List<Funcionario> Funcionarios { get; set; }
        public IEnumerable<Requisicao> Requisicoes { get; set; }

        public string AbrirIntervencao(int ocorrenciaId)
        {
            var ocorrencia = this.Ocorrencias.FirstOrDefault(o => o.Id == ocorrenciaId);

            if (ocorrencia.Custo > this.Verba)
            {
                return "Custo de ocorrencia excede a verba do setor.";
            }

            int funcionariosDisponivel = 0;
            for (int i = 0; i < Funcionarios.Count; i++)
            {
                if (Funcionarios[i].Disponivel)
                {
                    funcionariosDisponivel += 1;
                }
            }
            if (ocorrencia.Funcionarios > funcionariosDisponivel)
            {
                return "Mão de obra necessaria excede a quantidade atual disponível do setor.";
            }

            if (ocorrencia.Status == "Aprovada")
            {
                return "Não pode aprovar uma ocorrência que já foi aprovada.";
            }

            ocorrencia.Status = "Aprovada";
            // TODO: Intervencao
            return "Ocorrência aprovada com sucesso.";
        }

        public Requisicao SolicitarVerba(double valor)
        {
            Requisicao requisicao = new Requisicao();
            requisicao.Tipo = "Verba";
            requisicao.Setor = this;
            requisicao.Prefeitura = this.Prefeitura;
            requisicao.Data = DateTime.Today;
            requisicao.Verba = valor;

            this.Prefeitura.Requisicoes.Add(requisicao);
            return requisicao;
        }

        public Requisicao SolicitarMaisFuncionarios(int quantidade)
        {
            Requisicao requisicao = new Requisicao();
            requisicao.Tipo = "Contratação";
            requisicao.Setor = this;
            requisicao.Prefeitura = this.Prefeitura;
            requisicao.Data = DateTime.Today;
            requisicao.Funcionarios = quantidade;

            this.Prefeitura.Requisicoes.Add(requisicao);
            return requisicao;
        }
    }
}
