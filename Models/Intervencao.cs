using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppMinhaBahia.Models
{
    public class Intervencao
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public ICollection<Funcionario> FuncionariosAlocados { get; set; }
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
    }
}
