using AppMinhaBahia.Data;
using Microsoft.AspNetCore.Mvc;

namespace AppMinhaBahia.Controllers;

public class GovernadorController : Controller
{
    /* Este repositorio generico vai ser do tipo Governador, ja que como 
    estamos no controller do governador, vamos realizar operacoes relacionadas 
    a ele */ 
    /* Pegar o contexto do banco como parametro e repassar ele para o 
    repositorio generico em vez de fazer uso dele diretamente */ 
    public GovernadorController(MinhaBahiaContext context)
    {
    }

    /* Retornar a view padrao do controller para o caminho "/" */ 
    public IActionResult Index()
    {
        return View();
    }
}