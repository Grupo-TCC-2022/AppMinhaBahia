using System;
using System.Collections.Generic;

namespace AppMinhaBahia.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public ICollection<Ocorrencia> Ocorrencias { get; set; }

        public Ocorrencia ReportarOcorrencia(string tipo, string descricao, string localEspecifico)
        {
            Ocorrencia ocorrencia = new Ocorrencia();
            ocorrencia.Tipo = tipo;
            ocorrencia.Descricao = descricao;
            ocorrencia.Usuario = this;
            ocorrencia.LocalEspecifico = localEspecifico;
            ocorrencia.Data = DateTime.Today;

            this.Ocorrencias.Add(ocorrencia);
            return ocorrencia;
        }
    }
}
