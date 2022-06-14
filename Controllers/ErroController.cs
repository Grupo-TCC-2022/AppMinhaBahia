using AppMinhaBahia.Data;
using AppMinhaBahia.Interfaces;
using AppMinhaBahia.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppMinhaBahia.Controllers;

public class ErroController : Controller
{
    private readonly IRepositorioGenerico<Usuario> repositorio;
    public ErroController(MinhaBahiaContext context)
    {
        repositorio = new RepositorioGenerico<Usuario>(context);
    }

    public IActionResult Index(string mensagem)
    {
        /* Pegar o id do usuario logado */
        int usuarioID = Int32.Parse(User.FindFirst("ID").Value);
        /* Pesquisar no repositorio este usuario pelo ID */
        var usuarioLogado = repositorio.RetornarPorId(usuarioID);

        if (usuarioLogado is Governador)
        {
            ViewBag.Cargo = 'G';
        }
        else if (usuarioLogado is Prefeito)
        {
            ViewBag.Cargo = 'P';
        }
        else
        {
            ViewBag.Cargo = 'U';
        }

        ViewBag.MensagemErro = mensagem;

        return View();
    }
}