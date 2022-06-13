using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppMinhaBahia.Models;

[Table("Usuarios")]
public class Usuario
{
    /*______________CHAVE_PRIMARIA______________*/
    [Key]
    public int UsuarioID { get; set; }

    /*______________ATRIBUTOS_DA_CLASSE______________*/
    [Display(Name = "Nome completo")]
    [MinLength(3, ErrorMessage = "Nome inválido, informe no mínimo 3 caracteres")]
    [MaxLength(100, ErrorMessage = "Nome excedeu o tamanho permitido")]
    [Required(ErrorMessage = "Este campo é obrigatório")]
    public string NomeCompleto { get; set; }
    [Required(ErrorMessage = "Este campo é obrigatório")]
    [ValidarCPF(ErrorMessage = "CPF Inválido")]
    public string CPF { get; set; }
    [DataType(DataType.Password)]
    [MinLength(3, ErrorMessage = "Senha inválida, informe no mínimo 3 caracteres")]
    [MaxLength(50, ErrorMessage = "Senha excedeu o tamanho permitido")]
    [Required(ErrorMessage = "Este campo é obrigatório")]
    public string Senha { get; set; }

    /*______________USUARIO_MORA_EM_UMA_CIDADE______________*/
    /* Sei que prefeito herda de "Usuario", que ja tem o atributo cidade, 
    mas aqui a diferenca é que o prefeito tem uma cidade que ele gerencia 
    mesmo que migre para outra cidade, ja o usuario comum pode trocar de 
    cidade a vontade */
    [Display(Name = "Cidade de residência")]
    [ForeignKey("Cidade")]
    public int CidadeResidenciaID { get; set; }
    public Cidade CidadeResidencia { get; set; }

    /*______________USUARIO_FAZ_REQUISICOES_AOS_SEUS_SUPERIORES______________*/
    public ICollection<Requisicao> MinhasRequisicoes { get; set; } = new List<Requisicao>();
    public int? QuantidadeRequisicoes { get { return MinhasRequisicoes.Count; } set {} }
}
