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

        public Dictionary<string, string> AbrirIntervencao(int ocorrenciaId, string descricao)
        {
            var ocorrencia = this.Ocorrencias.FirstOrDefault(o => o.Id == ocorrenciaId);
            Dictionary<string, string> situacao = new Dictionary<string,string>();

            if (ocorrencia.Custo > this.Verba)
            {
                situacao.Add("status", "erro");
                situacao.Add("mensagem", "Custo de ocorrencia excede a verba do setor");
                return situacao;
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
                situacao.Add("status", "erro");
                situacao.Add("mensagem", "Mão de obra necessaria excede a quantidade atual disponível do setor");
                return situacao;
            }

            if (ocorrencia.Status == "Aprovada")
            {
                situacao.Add("status", "erro");
                situacao.Add("mensagem", "Não pode aprovar uma ocorrência que já foi aprovada");
                return situacao;
            }

            ocorrencia.Status = "Aprovada";
            
            Intervencao intervencao = new Intervencao();
            intervencao.Descricao = descricao;
            intervencao.Data = DateTime.Today;

            int funcionariosNecessarios = (int) ocorrencia.Funcionarios;

            for (int i = 0; i < this.Funcionarios.Count; i++)
            {
                var funcionario = this.Funcionarios[i];
                int jaAlocados = intervencao.FuncionariosAlocados.Count;

                if (funcionario.Disponivel && jaAlocados < funcionariosNecessarios)
                {
                    funcionario.Disponivel = false;
                    funcionario.Intervencao = intervencao;
                    intervencao.FuncionariosAlocados.Add(this.Funcionarios[i]);
                }
            }

            situacao.Add("status", "sucesso");
            situacao.Add("mensagem", "Ocorrência aprovada com sucesso");
            return situacao;
        }

        public Requisicao SolicitarVerbaMunicipal(double valor)
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
