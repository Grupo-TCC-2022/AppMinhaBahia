using System.Linq;
using System.Security.Claims;
using AppMinhaBahia.Data;
using AppMinhaBahia.Interfaces;
using AppMinhaBahia.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppMinhaBahia.Controllers;

public class PrefeitoController : Controller
{
    /* Este repositorio generico vai ser do tipo Prefeito, ja que como 
    estamos no controller do prefeito, vamos realizar operacoes relacionadas 
    a ele */ 
    private readonly IRepositorioGenerico<Prefeito> repositorio;
    private readonly IRepositorioGenerico<Cidade> repositorio_cidade;
    private readonly IRepositorioGenerico<Governador> repositorio_governador;
    /* Pegar o contexto do banco como parametro e repassar ele para o 
    repositorio generico em vez de fazer uso dele diretamente */ 
    public PrefeitoController(MinhaBahiaContext context)
    {
        repositorio = new RepositorioGenerico<Prefeito>(context);
        repositorio_cidade = new RepositorioGenerico<Cidade>(context);
        repositorio_governador = new RepositorioGenerico<Governador>(context);
    }

    /* Caminho encontrado quando o usuario faz um requisicao com o 
    metodo "POST" para o caminho "/Prefeito/Cadastrar */
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar([Bind("NomeCompleto, CPF, Senha, CidadeResidenciaID, CidadeGovernoID")] Prefeito novoPrefeito)
    {
        if (ModelState.IsValid)
        {
            /* Usando o repositorio generico para retornar uma colecao de todos os usuarios 
            e depois "filtrando" essa lista para ver se encontra alguem com o CPF que 
            veio da View */ 
            var prefeitoExiste = repositorio.RetornarTabela().FirstOrDefault(p => p.CPF == novoPrefeito.CPF);

            /* Se aquela pesquisa no banco por CPF do prefeito retornar algo esse "if" 
            vai ser verdadeiro e vai entrar nele */ 
            if (prefeitoExiste != null)
            {
                ModelState.AddModelError("CPF", "CPF já cadastrado no sistema");
                return View(novoPrefeito);
            }
            /* Se aquela cidade ja tem prefeito - erro */
            var prefeitoCidadeExiste = repositorio.RetornarTabela().FirstOrDefault(p => p.CidadeGovernoID == novoPrefeito.CidadeGovernoID);
            if (prefeitoCidadeExiste != null)
            {
                ModelState.AddModelError("CidadeGovernoID", "Esta Cidade já possui prefeito cadastrado");
                return View(novoPrefeito);
            }
            /* Passou pela validacao acima, logo pode cadastrar, usando o repositorio 
            pronto no caso */
            /* Assumindo que o prefeito inicialmente governe a cidade que ele more */
            /* Atribuindo cidade ao prefeito */
            var cidade = repositorio_cidade.RetornarPorId(novoPrefeito.CidadeGovernoID);
            novoPrefeito.CidadeGoverno = cidade;
            cidade.LiderID = novoPrefeito.UsuarioID;
            repositorio.Cadastrar(novoPrefeito);
            repositorio.Salvar();
            cidade.LiderID = novoPrefeito.UsuarioID;
            repositorio.Salvar();
            // Redirecionar para index de prefeitos
            return RedirectToAction("Index", "Home");
        }
        return View(novoPrefeito);
    }

    /* Retornar a view de cadastro (QUANDO O METODO FOR GET) para o 
    caminho "/Prefeito/Cadastrar" */ 
    public IActionResult Cadastrar()
    {
        return View();
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Listar()
    {
        int governadorID = Int32.Parse(User.FindFirst("ID").Value);
        /* Pesquisar no repositorio este usuario pelo ID */
        var governadorLogado = repositorio_governador.RetornarPorId(governadorID);

        /* Pegar somentente os prefeitos que possuem cidade dentro do governo do governador logado */
        var prefeitosDoEstado = repositorio.RetornarTabela()
        .Include(p => p.CidadeResidencia)
        .Include(p => p.CidadeGoverno)
        .Include(p => p.MinhasRequisicoes)
        .Where(p => p.CidadeResidencia.EstadoID == governadorLogado.EstadoGovernoID)
        .ToList();

        return View(prefeitosDoEstado);
    }
}