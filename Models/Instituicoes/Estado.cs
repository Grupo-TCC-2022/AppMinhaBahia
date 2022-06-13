using System.ComponentModel.DataAnnotations.Schema;

namespace AppMinhaBahia.Models;

[Table("Estados")]
public class Estado : Instituicao
{
    /* Esta classe herda da classe "Instituicao", a qual ja tem uma 
    chave primaria e nome, quando o entity framework mapeia as 
    classes que usam heranca, ele cria uma tabela singular para 
    todas as classes que herdam daquela classe, la elas sao 
    diferencias pelo atributo "Discriminator" */

    /*______________ESTADO_TEM_MUITAS_CIDADES______________*/
    public ICollection<Cidade> Cidades { get; set; }
}
