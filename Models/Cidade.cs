using System.ComponentModel.DataAnnotations;

namespace AppMinhaBahia.Models
{
    public class Cidade
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
    }
}
