using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppMinhaBahia.Models;

[Table("Instituicoes")]
public class Instituicao
{
    /*______________CHAVE_PRIMARIA______________*/
    [Key]
    public int InstituicaoID { get; set; }

    /*______________ATRIBUTOS_DA_CLASSE______________*/
    [Required(ErrorMessage = "Este campo é obrigatorio")]
    public string Nome { get; set; }
    public double Verba { get; set; }

    /*______________INSTITUICAO_TEM_LIDER______________*/
    [ForeignKey("Usuario")]
    public int? LiderID { get; set; }

    /*________INSTITUICAO_TEM_MUITAS_REQUISICOES_FEITAS_PELAS_INSTITUICOES_ABAIXO________*/
    public ICollection<Requisicao> Requisicoes { get; set; }
}
