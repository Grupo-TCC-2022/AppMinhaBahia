using System;
using System.Collections.Generic;

namespace AppMinhaBahia.Models
{
    public class Intervencao
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public ICollection<Funcionario> FuncionariosAlocados { get; set; }
        public DateTime Data { get; set; }
    }
}
