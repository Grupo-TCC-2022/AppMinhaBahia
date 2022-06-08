using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppMinhaBahia.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nome Completo")]
        [DataType(DataType.Text)]
        [MinLength(3,ErrorMessage ="Nome inválido, informe no mínimo 3 caracteres")]
        [MaxLength(50, ErrorMessage = "Nome excedeu o tamanho permitido")]
        [Required]
        public string NomeCompleto { get; set; }
        [Required]
        [ValidacaoDeCPF]
        public string CPF { get; set; }
        [DataType(DataType.Password)]
        [MinLength(3,ErrorMessage ="Senha inválida, informe no mínimo 3 caracteres")]
        [MaxLength(50, ErrorMessage = "Senha excedeu o tamanho permitido")]
        public string Senha { get; set; }
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
