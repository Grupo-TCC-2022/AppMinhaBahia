using System;

namespace AppMinhaBahia.Models
{
    public class Requisicao
    {
        public int Id { get; set; }
        // Fundos ou Contratação
        public string Tipo { get; set; }
        public int SetorId { get; set; }
        public Setor Setor { get; set; }
        public int PrefeituraId { get; set; }
        public Prefeitura Prefeitura { get; set; }
        public DateTime Data { get; set; }
        public double? Fundos { get; set; }
        public int? Funcionarios { get; set; }
    }
}
