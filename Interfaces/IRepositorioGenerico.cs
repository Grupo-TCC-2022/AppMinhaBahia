using Microsoft.EntityFrameworkCore;

namespace AppMinhaBahia.Interfaces;
/* Interface responsavel por definir quais operacoes um repositorio 
generico (um repositorio de qualquer entidade, Usuario, Prefeito, Cidade...) 
pode realizar com o banco */
interface IRepositorioGenerico<T> where T : class
{
    DbSet<T> RetornarTabela();
    T RetornarPorId(int id);
    void Cadastrar(T qualquerEntidade);
    void Atualizar(T qualquerEntidade);
    void Deletar(int id);
    void Salvar();
}

/* FONTE: https://www.linkedin.com/pulse/repository-pattern-c-pawan-verma/ */
