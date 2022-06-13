using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppMinhaBahia.Models;

[Table("Secretarios")]
public class Secretario : Usuario
{
    /* Herdando da classe "Usuario", essa 
    classe ja "ganha" o atributo ID como chave primaria */

    /*______________SECRETARIO_TEM_UM_SETOR______________*/
    [Display(Name = "Setor de administração")]
    [ForeignKey("Setor")]
    public int SetorAdministracaoID { get; set; }
    public Setor SetorAdministracao { get; set; }

    /* Esta classe herda da classe "Usuario", logo possui o 
    atributo "Requisicoes", que o usuario faz aos seus superiores */
}
