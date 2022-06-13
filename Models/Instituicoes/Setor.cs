using System.ComponentModel.DataAnnotations.Schema;

namespace AppMinhaBahia.Models;

[Table("Setores")]
public class Setor : Instituicao
{
    /* Esta classe herda da classe "Instituicao", a qual ja tem uma 
    chave primaria e nome, quando o entity framework mapeia as 
    classes que usam heranca, ele cria uma tabela singular para 
    todas as classes que herdam daquela classe, la elas sao 
    diferencias pelo atributo "Discriminator" */

    /*______________SETOR_FICA_EM_UMA_CIDADE______________*/
    [ForeignKey("Cidade")]
    public int CidadeID { get; set; }
    public Cidade Cidade { get; set; }

    /*______________SETOR_POSSUI_MUITOS_FUNCIONARIOS______________*/
    public ICollection<Funcionario> Funcionarios { get; set; }
}
