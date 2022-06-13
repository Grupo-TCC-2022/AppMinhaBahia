using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppMinhaBahia.Models;

[Table("Requisicoes")]
public class Requisicao
{
    /*______________CHAVE_PRIMARIA______________*/
    [Key]
    public int RequisicaoID { get; set; }

    /*______________ATRIBUTOS_DA_CLASSE______________*/
    public TipoRequisicao Tipo { get; set; }
    [Display(Name = "Descrição")]
    [Required(ErrorMessage = "Este campo é obrigatorio")]
    [MaxLength(2000, ErrorMessage = "Excedeu o tamanho permitido")]
    [DataType(DataType.MultilineText)]
    public string Descricao { get; set; }
    public double? Verba { get; set; } // Verba se o tipo for verba 
    public DateTime DataDeCriacao { get; set; } = DateTime.Now;
    public StatusRequisicao Status { get; set; } = StatusRequisicao.analise;

    /*________REQUISICAO_EH_FEITA_POR_ALGUMA_PESSOA_PARA_SEU_SUPERIOR________*/
    /*____ABAIXO_ESTA_A_PESSOA_CRIADORA_DA_REQUISICAO____*/
    [ForeignKey("Usuario")]
    public int UsuarioID { get; set; }
    public virtual Usuario Usuario { get; set; }

    /*______________TODA_REQUISICAO_TEM_UMA_INSTITUICAO_ALVO______________*/
    [ForeignKey("Instituicao")]
    public int InstituicaoID { get; set; }
    public virtual Instituicao Instituicao { get; set; }
}

/*______________TIPOS_DA_REQUISICAO______________*/
public enum TipoRequisicao
{
    verba,
    ocorrencia
}

/*______________STATUS_DA_REQUISICAO______________*/
public enum StatusRequisicao
{
    analise,
    aprovada,
    reprovada
}
