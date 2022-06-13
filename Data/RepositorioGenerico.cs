using AppMinhaBahia.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AppMinhaBahia.Data;
/* Classe responsavel por realizar toda comunicacao com o banco 
relacionado a toda e qualquer entidade */

/* Aqui em especifico esta sendo usado "Generics", um esquema no C# 
que permite criar classes que performam operacoes para qualquer classe 
generica (ou um tipo de classe definida). Basicamente aqueles "T's" que 
voces estao vendo abaixo representam qualquer classe */

/* Numa linguagem mais direta, aqui estou dizendo: quero uma classe chamada 
RepositorioGenerico, essa classe vai tratar de um tipo generico "T" (o nome 
pode ser qualquer um), esse tipo "T" estou me referindo ao mesmo da interface 
IRepositorioGenerico que tem o "T" tbm, e no final digo, "where T : class", 
essa expressao basicamente diz "onde esse T vai ser do tipo class", todas as 
classes do C# herdam do tipo "class", logo todas elas sao uma class (pense como 
o "object" em que todos os objetos herdam dele logo sao object), entao como 
esse "T" representa qualquer classe do programa posso aplicar todas as operacoes 
aqui a qualquer classe */
public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : class
{
    /* A primeira versao do programa foi feita sem esse recurso e como resultado 
    foi criado 6 (seis) repositorios para manusear o banco, todos eles exatamente 
    com as mesmas operacoes abaixo, gerando duplicacao de codigo desnecessaria */

    private MinhaBahiaContext _context = null;
    private DbSet<T> tabelaGenerica = null;
    /* Recebendo o contexto do banco */
    /* Gerando uma tabela generica representado qualquer tabela do banco */
    public RepositorioGenerico(MinhaBahiaContext context)
    {
        this._context = context;
        tabelaGenerica = this._context.Set<T>();
    }

    /* Apenas retornando todos os T's genericos cadastrados */
    public DbSet<T> RetornarTabela()
    {
        return tabelaGenerica;
    }

    /* Retornando um unico T baseado no id que veio por parametro */
    /* OBS.: O metodo "Find()" busca no banco um T pelo id passado por 
    parametro comparando as chaves primarias do banco, estou usando ele 
    ao inves do "FirstOrDefault()" porque pelo que eu entendi ele eh melhor 
    otimizado para colecoes grandes como num banco, o que impacta na 
    performance */
    public T RetornarPorId(int id)
    {
        return tabelaGenerica.Find(id);
    }

    /* Simples insercao de T no banco usando o medoto "Add()" */
    public void Cadastrar(T qualquerEntidade)
    {
        tabelaGenerica.Add(qualquerEntidade);
    }

    /* Edicao de um T no banco */
    public void Atualizar(T qualquerEntidade)
    {
        tabelaGenerica.Attach(qualquerEntidade);
        _context.Entry(qualquerEntidade).State = EntityState.Modified;
    }

    /* Buscando um T no banco pelo id dele usando o metodo "Find()" e 
    em seguida deletando ele usando o metodo "Remove()" */
    public void Deletar(int id)
    {
        T qualquerEntidade = tabelaGenerica.Find(id);
        tabelaGenerica.Remove(qualquerEntidade);
    }

    /* As modificacoes no banco nao sao aplicadas a menos que voce chame o metodo 
    "SaveChanges()" */
    public void Salvar()
    {
        _context.SaveChanges();
    }
}

/* OBS.: Para quem nao conhece esse design de "Repositorios", basicamente 
ele eh uma camada que separa os Controllers no MVC das operacoes 
relacionadas com o banco de dados (exemplo: INSERT, DELETE, UPDATE...). 
O beneficio desta abordagem eh a separacao das partes relacionadas a 
regras de negocio (Controllers), entidades (models) e a comunicao com 
o banco (Repositorios) assim cada qual fica responsavel por apenas uma 
tarefa, se voce modificar algo na logica de acesso ao banco nao precisara 
modificar nada no resto do programa, o mesmo para as regras de negocio, o 
que ajuda na manutencao do programa, sem falar da estetica que melhora 
muito */
/* FONTE: https://www.linkedin.com/pulse/repository-pattern-c-pawan-verma/ */ 
