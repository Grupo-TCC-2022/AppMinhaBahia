using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppMinhaBahia.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nome completo")]
        [MinLength(3, ErrorMessage ="Nome inválido, informe no mínimo 3 caracteres")]
        [MaxLength(50, ErrorMessage = "Nome excedeu o tamanho permitido")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public string NomeCompleto { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public string CPF { get; set; }
        [Display(Name = "Nome da cidade")]
        public Cidade Cidade { get; set; }
        [DataType(DataType.Password)]
        [MinLength(3, ErrorMessage ="Senha inválida, informe no mínimo 3 caracteres")]
        [MaxLength(50, ErrorMessage = "Senha excedeu o tamanho permitido")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public string Senha { get; set; }
        public ICollection<Ocorrencia> Ocorrencias { get; set; }

        public void ReportarOcorrencia(Ocorrencia ocorrencia, Prefeitura prefeitura)
        {   
            prefeitura.Ocorrencias.Add(ocorrencia);
            this.Ocorrencias.Add(ocorrencia);
        }
    }
}
