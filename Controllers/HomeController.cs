using AppMinhaBahia.Data;
using AppMinhaBahia.Interfaces;
using AppMinhaBahia.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppMinhaBahia.Controllers;

/* O proposito deste controller nesta aplicacao nao vai ser o de retornar, 
mas o de redirecionar o Usuario para sua determinada View dependendo 
do cargo do Usuario */
public class HomeController : Controller
{
    /* Este repositorio generico vai ser do tipo Usuario */ 
    private readonly IRepositorioGenerico<Usuario> repositorio;
    /* Pegar o contexto do banco como parametro e repassar ele para o 
    repositorio generico em vez de fazer uso dele diretamente */ 
    public HomeController(MinhaBahiaContext context)
    {
        repositorio = new RepositorioGenerico<Usuario>(context);
    }

    public IActionResult Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            /* Pegar o id do usuario logado */
            int usuarioID = Int32.Parse(User.FindFirst("ID").Value);
            /* Pesquisar no repositorio este usuario pelo ID */
            var usuarioLogado = repositorio.RetornarPorId(usuarioID);
            /* O trabalho de redirecionar eh feito pela funcao chamada */
            return RedirecionarUsuario(usuarioLogado);
        }
        /* Redirecione o usuario para a view de cadastro caso nao esteja 
        niguem logado */
        return RedirectToAction("Index", "Usuario");
    }

    /* Funcao que tem como objetivo redirecionar o Usuario logado cada 
    qual para sua View especifica, o Governador vai para sua view onde 
    ele pode cadastrar e editar Prefeitos, os Prefeitos vao para sua 
    View onde podem trabalhar com os setores e assim adiante... */
    public RedirectToActionResult RedirecionarUsuario(Usuario usuario)
    {
        if (usuario is Governador)
        {
            return RedirectToAction("Index", "Governador");
        }
        else if (usuario is Prefeito)
        {
            return RedirectToAction("Index", "Prefeito");
        }
        else if (usuario is Secretario)
        {
            return RedirectToAction("Index", "Prefeito");
        }
        else if (usuario is Funcionario)
        {
            return RedirectToAction("Index", "Funcionario");
        }

        return RedirectToAction("Index", "Usuario");
    }
}