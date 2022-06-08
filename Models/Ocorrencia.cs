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
        public double CustoEstimado { get; set; }
        public int MaoDeObraNecessaria { get; set; }
        public DateTime Data { get; set; }
    }
}
