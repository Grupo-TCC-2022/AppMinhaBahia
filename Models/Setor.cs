using System;
using System.Collections.Generic;
using System.Linq;

namespace AppMinhaBahia.Models
{
    public class Setor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Verba { get; set; }
        public int PrefeituraId { get; set; }
        public Prefeitura Prefeitura { get; set; }
        public List<Funcionario> Funcionarios { get; set; }
        public ICollection<Ocorrencia> Ocorrencias { get; set; }
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
            return "Ocorrência Aprovada com sucesso.";
        }

        public Requisicao PedirVerba(double valor)
        {
            Requisicao requisicao = new Requisicao();
            requisicao.Tipo = "Verba";
            requisicao.Setor = this;
            requisicao.Prefeitura = this.Prefeitura;
            requisicao.Data = DateTime.Today;
            requisicao.Verba = valor;

            return requisicao;
        }

        public Requisicao PedirMaisFuncionarios(int quantidade)
        {
            Requisicao requisicao = new Requisicao();
            requisicao.Tipo = "Contratação";
            requisicao.Setor = this;
            requisicao.Prefeitura = this.Prefeitura;
            requisicao.Data = DateTime.Today;
            requisicao.Funcionarios = quantidade;

            return requisicao;
        }
    }
}
