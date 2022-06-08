using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AppMinhaBahia.Models
{
    public class Cidade
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
    }
}
