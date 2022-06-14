using AppMinhaBahia.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AppMinhaBahia.Data;
public class MinhaBahiaContext : DbContext
{
    /* Aqui defino quais classes poderao ser chamadas por objetos do tipo desta classe 
    (MinhaBahiaContext), como esta classe herda da classe "DbContext" do EntityFramework, 
    qualquer objeto do tipo dela tem acesso ao "contexto" do banco, podem adicionar, remover, editar... qualquer classe (que vai virar tabela) listada abaixo dentro do DbSet<...>. */
    /* OBS.: As modificacoes feitas por objetos deste tipo so serao salvas no banco 
    quando o metodo "SaveChangesAsync()" for chamado */
    public DbSet<Cidade> Cidades { get; set; }
    public DbSet<Estado> Estados { get; set; }
    public DbSet<Governador> Governadores { get; set; }
    public DbSet<Prefeito> Prefeitos { get; set; }
    public DbSet<Requisicao> Requisicoes { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        /*________ABAIXO_SEGUE_OS_SEMEADORES_DE_DADOS_PARA_TESTE_DO_PROGRAMA________*/
        builder.Entity<Estado>().HasData(
            new Estado()
            {
                InstituicaoID = 1,
                Nome = "Bahia",
                Verba = Double.MaxValue - 1,
                LiderID = 1
            }
        );

        /* Pegando o array de cidades presente na chave "ListaDeCidadesBA" no 
        "appsettings.json" local. */
        /* FONTE: https://servicodados.ibge.gov.br/api/v1/localidades/estados/BA/distritos */
        var arrayDeCidades = _configuration.GetSection("ListaDeCidadesBA").Get<string[]>();
        builder.Entity<Cidade>().HasData(
            arrayDeCidades.Select((nomeDaCidade, indexDoArray) => new Cidade()
            {
                InstituicaoID = indexDoArray + 2,
                Nome = nomeDaCidade,
                Verba = 1000000,
                EstadoID = 1
            })
        );

        /* Adicionando um governador de testes para o estado da Bahia */
        builder.Entity<Governador>().HasData(
            new Governador() {
                UsuarioID = 1,
                NomeCompleto = "Wendel Simões dos Santos",
                CPF = "admin",
                Senha = "admin",
                CidadeResidenciaID = 152,
                EstadoGovernoID = 1
            }
        );
    }

    private readonly IConfiguration _configuration;

    /* Nao foque nessa parte, eh apenas um construtor que iniciara essa classe como 
    um "DbContext" (um manipulador do banco de dados), ele faz isso passando a chave 
    de conexao pelo parametro options abaixo. Ja o configuration do tipo "IConfiguration" 
    que esta sendo passado abaixo é responsavel por pegar os nomes das cidades no estado 
    da Bahia, esses nomes estao armazenados no appsettings.json na forma de array, tudo que 
    faco eh pegar esse array usando o configuration e usando ele para semear no banco 
    todas as cidades */

    public MinhaBahiaContext(DbContextOptions options, IConfiguration configuration) : base
    (options)
    {
        _configuration = configuration;
    }
}
