using System;

namespace AppMinhaBahia.Models
{
    public class Ocorrencia
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public string LocalEspecifico { get; set; }
        public double? Custo { get; set; }
        public int? Funcionarios { get; set; }
        public DateTime Data { get; set; }
        public int IntervencaoId { get; set; }
        public Intervencao Intervencao { get; set; }
        public int PrefeituraId { get; set; }
        public Prefeitura Prefeitura { get; set; }
        // Em analize, Encaminhada, Aprovada ou Arquivada
        public string Status { get; set; }

        public Ocorrencia()
        {
            this.Status = "Em analize";
        }
    }
}
