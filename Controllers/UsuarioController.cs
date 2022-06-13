using System.Linq;
using System.Security.Claims;
using AppMinhaBahia.Data;
using AppMinhaBahia.Interfaces;
using AppMinhaBahia.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AppMinhaBahia.Controllers;

public class UsuarioController : Controller
{
    /* Este repositorio generico vai ser do tipo Usuario, ja que como 
    estamos no controller do usuario, vamos realizar operacoes relacionadas 
    a ele */ 
    private readonly IRepositorioGenerico<Usuario> repositorio;
    /* Pegar o contexto do banco como parametro e repassar ele para o 
    repositorio generico em vez de fazer uso dele diretamente */ 
    public UsuarioController(MinhaBahiaContext context)
    {
        repositorio = new RepositorioGenerico<Usuario>(context);
    }

    /* Caminho encontrado quando o usuario faz um requisicao com o 
    metodo "POST" para p caminho "/Usuario/Cadastrar */
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar([Bind("NomeCompleto, CPF, Senha, CidadeResidenciaID")] Usuario usuario)
    {
        if (ModelState.IsValid)
        {
            /* Usando o repositorio generico para retornar uma colecao de todos os usuarios 
            e depois "filtrando" essa lista para ver se encontra alguem com o CPF que 
            veio da View */ 
            var usuarioExiste = repositorio.RetornarTabela().FirstOrDefault(u => u.CPF == usuario.CPF);

            /* Se aquela pesquisa no banco por CPF do usuario retornar algo esse "if" 
            vai ser verdadeiro e vai entrar nele */ 
            if (usuarioExiste != null)
            {
                /* Como o entity framework core so implementa o DataAnnotation "Index" 
                que eh usado para definir campos unicos no banco na sua versao 6+, 
                a gente tem que adicionar ao ModelState uma validacao manualmente */
                ModelState.AddModelError("CPF", "CPF já cadastrado no sistema");
                return View(usuario);
            }
            
            /* Passou pela validacao acima, logo pode cadastrar, usando o repositorio 
            pronto no caso */
            repositorio.Cadastrar(usuario);
            repositorio.Salvar();
            return RedirectToAction("Index", "Usuario");
        }
        return View(usuario);
    }

    /* Caminho encontrado quando o usuario faz um requisicao com o 
    metodo "POST" para p caminho "/Usuario/Login */
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string CPF, string senha)
    {
        var usuarioExiste = repositorio.RetornarTabela().FirstOrDefault(u => u.CPF == CPF);
        
        if (CPF == null)
        {
            ModelState.AddModelError("CPF", "Este campo é obrigatorio");
            return View(usuarioExiste);
        }

        if (usuarioExiste == null)
        {
            ModelState.AddModelError("CPF", "Usuário não encontrado");
            return View(usuarioExiste);
        }

        if (usuarioExiste.Senha != senha)
        {
            ModelState.AddModelError("Senha", "Senha invalida");
            return View(usuarioExiste);
        }
        
        List<Claim> direitosAcesso = new List<Claim>{
            new Claim("ID", usuarioExiste.UsuarioID.ToString())
        };
        
        var identidade = new ClaimsIdentity(direitosAcesso, "Identity.Login");
        var usuarioPrincipal = new ClaimsPrincipal(new[] { identidade });

        await HttpContext.SignInAsync(usuarioPrincipal, new AuthenticationProperties
        {
            IsPersistent = false,
            ExpiresUtc = DateTime.Now.AddHours(1)
        });
        
        return RedirectToAction("Index", "Home");
    }

    /* Retornar a view de cadastro (QUANDO O METODO FOR GET) para o 
    caminho "/Usuario/Cadastrar" */ 
    public IActionResult Cadastrar()
    {
        return View();
    }

    /* Retornar a view de login (QUANDO O METODO FOR GET) para o 
    caminho "/Usuario/Login" */ 
    public IActionResult Login()
    {
        return View();
    }

    /* Retornar a view padrao do controller usuario para o caminho "/" */ 
    public IActionResult Index()
    {
        /* Se usuario tiver logado, vai para a pagina padrao dele, se nao 
        for o caso, ele "pula" esse IF e vai direto para sua View de Login */
        if (User.Identity.IsAuthenticated)
        {
            // /* Pegar o id do usuario logado */
            // int usuarioID = Int32.Parse(User.FindFirst("ID").Value);
            // /* Pesquisar no repositorio este usuario pelo ID */
            // var usuarioLogado = repositorio.RetornarPorId(usuarioID);
            // /* O trabalho de redirecionar eh feito pela funcao chamada */
            return View();
        }
        return RedirectToAction("Login", "Usuario");
    }
}