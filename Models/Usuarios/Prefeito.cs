using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppMinhaBahia.Models;

[Table("Prefeitos")]
public class Prefeito : Usuario
{
    /* Herdando da classe "Usuario", essa 
    classe ja "ganha" o atributo ID como chave primaria */
    
    [Display(Name = "Cidade de governo")]
    [ForeignKey("Cidade")]
    public int CidadeGovernoID { get; set; }
    public Cidade CidadeGoverno { get; set; }

    /* Esta classe herda da classe "Usuario", logo possui o 
    atributo "Requisicoes", que o usuario faz aos seus superiores */
}
