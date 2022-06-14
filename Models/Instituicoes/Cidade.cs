using System.ComponentModel.DataAnnotations.Schema;

namespace AppMinhaBahia.Models;

[Table("Cidades")]
public class Cidade : Instituicao
{
    /* Esta classe herda da classe "Instituicao", a qual ja tem uma 
    chave primaria e nome, quando o entity framework mapeia as 
    classes que usam heranca, ele cria uma tabela singular para 
    todas as classes que herdam daquela classe, la elas sao 
    diferencias pelo atributo "Discriminator" */

    /*______________CIDADE_ESTA_EM_UM_ESTADO______________*/
    [ForeignKey("Estado")]
    public int EstadoID { get; set; }
    public Estado Estado { get; set; }

    /*______________CIDADE_TEM_MUITOS_RESIDENTES______________*/
    public ICollection<Usuario> Residentes { get; set; }
}
