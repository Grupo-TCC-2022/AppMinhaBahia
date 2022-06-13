using System.ComponentModel.DataAnnotations.Schema;

namespace AppMinhaBahia.Models;

[Table("Funcionarios")]
public class Funcionario : Usuario
{
    /* Herdando da classe "Usuario", essa 
    classe ja "ganha" o atributo ID como chave primaria */ 

    /*______________FUNCIONARIO_FAZ_PARTE_DE_UM_SETOR______________*/ 
    [ForeignKey("Setor")]
    public int SetorID { get; set; }
    public Setor Setor { get; set; }

    /* Esta classe herda da classe "Usuario", logo possui o 
    atributo "Requisicoes", que o usuario faz aos seus superiores */ 
}
