using System;
using System.ComponentModel.DataAnnotations;

namespace AppMinhaBahia.Models
{
    public class Ocorrencia
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [MinLength(3, ErrorMessage ="Informe no mínimo 3 caracteres")]
        [MaxLength(50, ErrorMessage = "Excedeu o tamanho permitido")]
        public string Tipo { get; set; }
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [MinLength(50, ErrorMessage ="Informe no mínimo 50 caracteres")]
        [MaxLength(1000, ErrorMessage = "Excedeu o tamanho permitido")]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        [Display(Name = "Local específico")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public string LocalEspecifico { get; set; }
        public double? Custo { get; set; }
        [Display(Name = "Funcionários")]
        public int? Funcionarios { get; set; }
        [DataType(DataType.Date)]
        public DateTime Data { get; set; } = DateTime.Now;
        public int? IntervencaoId { get; set; }
        public Intervencao Intervencao { get; set; }
        public int PrefeituraId { get; set; }
        public Prefeitura Prefeitura { get; set; }
        // Em analize, Encaminhada, Aprovada ou Arquivada
        public string Status { get; set; } = "Em analize";
    }
}
