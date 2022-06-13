using AppMinhaBahia.Data;
using AppMinhaBahia.Interfaces;
using AppMinhaBahia.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppMinhaBahia.Controllers;

public class CidadeController : Controller
{
    /* Este repositorio generico vai ser do tipo Cidade, ja que como 
    estamos no controller do cidade, vamos realizar operacoes relacionadas 
    a ele */ 
    private readonly IRepositorioGenerico<Cidade> repositorio;
    /* Pegar o contexto do banco como parametro e repassar ele para o 
    repositorio generico em vez de fazer uso dele diretamente */ 
    public CidadeController(MinhaBahiaContext context)
    {
        repositorio = new RepositorioGenerico<Cidade>(context);
    }

    /* Controller destinado para e unicamente enviar um Json (basicamente 
    uma mensagem com chaves e valores - de uma olhada no appsettings.json 
    para ver) para todas as views que necessitarem da lista completa 
    de cidades*/ 
    [HttpGet]
    public JsonResult GetCidades()
    {
        return Json(repositorio.RetornarTabela().ToList());
    }
}