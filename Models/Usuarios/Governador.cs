using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppMinhaBahia.Models;

[Table("Governadores")]
public class Governador : Usuario
{
    /* Herdando da classe "Usuario", essa 
    classe ja "ganha" o atributo ID como chave primaria */

    /*______________GOVERNADOR_TEM_UM_ESTADO______________*/
    [Display(Name = "Estado de governo")]
    [ForeignKey("Estado")]
    public int EstadoGovernoID { get; set; }
    public Estado EstadoGoverno { get; set; }

    /* Esta classe herda da classe "Usuario", logo possui o 
    atributo "Requisicoes", que o usuario faz aos seus superiores */
}
