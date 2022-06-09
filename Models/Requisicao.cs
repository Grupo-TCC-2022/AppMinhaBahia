using System;
using System.ComponentModel.DataAnnotations;

namespace AppMinhaBahia.Models
{
    public class Requisicao
    {
        public int Id { get; set; }
        // Verba ou Contratação
        public string Tipo { get; set; }
        public int SetorId { get; set; }
        public Setor Setor { get; set; }
        public int PrefeituraId { get; set; }
        public Prefeitura Prefeitura { get; set; }
        [DataType(DataType.Date)]
        public DateTime Data { get; set; } = DateTime.Now;
        public double? Verba { get; set; }
        public int? Funcionarios { get; set; }
        // Em analize, Aprovada ou Reprovada
        public string Status { get; set; }

        public Requisicao()
        {
            this.Status = "Em analize";
        }
    }
}
