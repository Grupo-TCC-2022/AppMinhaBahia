using AppMinhaBahia.Data;
using AppMinhaBahia.Interfaces;
using AppMinhaBahia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppMinhaBahia.Controllers;

public class RequisicaoController : Controller
{
    private readonly IRepositorioGenerico<Requisicao> repositorio;
    private readonly IRepositorioGenerico<Usuario> repositorio_usuario;
    private readonly IRepositorioGenerico<Prefeito> repositorio_prefeito;
    /* Pegar o contexto do banco como parametro e repassar ele para o 
    repositorio generico em vez de fazer uso dele diretamente */ 
    public RequisicaoController(MinhaBahiaContext context)
    {
        repositorio = new RepositorioGenerico<Requisicao>(context);
        repositorio_usuario = new RepositorioGenerico<Usuario>(context);
        repositorio_prefeito = new RepositorioGenerico<Prefeito>(context);
    }

    [Route("Requisicao/Index/{status}")]
    public IActionResult Index(string status)
    {   
        if (status == null || status == "")
        {
            return NotFound();
        }

        StatusRequisicao statusEnum;
        if (!Enum.TryParse(status, true, out statusEnum))
        {
            return NotFound();
        }
        
        var requisicoes = repositorio.RetornarTabela()
        .Where(r => r.Status == statusEnum)
        .Include(r => r.Instituicao)
        .Include(r => r.Usuario)
        .ThenInclude(u => u.CidadeResidencia)
        .AsQueryable();

        /* Pegar o id do usuario logado */
        int usuarioID = Int32.Parse(User.FindFirst("ID").Value);
        /* Pesquisar no repositorio este usuario pelo ID */
        var usuarioLogado = repositorio_usuario.RetornarPorId(usuarioID);

        if (usuarioLogado is Governador)
        {
            ViewBag.Cargo = 'G';
            requisicoes = requisicoes
            .Where(r => 
            r.Instituicao is Estado 
            && r.Instituicao.LiderID == usuarioLogado.UsuarioID 
            && r.Tipo == TipoRequisicao.verba);
        }
        else if (usuarioLogado is Prefeito)
        {
            ViewBag.Cargo = 'P';
            requisicoes = requisicoes
            .Where(r => 
            r.Instituicao is Cidade 
            && r.Instituicao.LiderID == usuarioLogado.UsuarioID 
            && r.Tipo == TipoRequisicao.ocorrencia);
        }
        else if (usuarioLogado is Secretario)
        {
            ViewBag.Cargo = 'S';
            requisicoes = requisicoes
            .Where(r => 
            r.Instituicao is Setor 
            && r.Instituicao.LiderID == usuarioLogado.UsuarioID 
            && r.Tipo == TipoRequisicao.ocorrencia);
        }
        else
        {
            ViewBag.Cargo = 'U';
        }

        return View(requisicoes.ToList());
    }

    [Route("Requisicao/Detalhes/{requisicaoid}")]
    public IActionResult Detalhes(int? requisicaoid)
    {
        if (requisicaoid == null)
        {
            return NotFound();
        }

        var requisicao = repositorio.RetornarPorId((int) requisicaoid);
        if (requisicao == null)
        {
            return NotFound();
        }

        int usuarioID = Int32.Parse(User.FindFirst("ID").Value);
        var usuarioLogado = repositorio_usuario.RetornarPorId(usuarioID);

        if (usuarioLogado is Governador)
        {
            ViewBag.Cargo = 'G';
            var prefeito_requisicao = repositorio_prefeito
            .RetornarTabela()
            .Include(p => p.CidadeGoverno)
            .FirstOrDefault(p => p.UsuarioID == requisicao.UsuarioID);
            ViewBag.CidadeRequisicao = prefeito_requisicao.CidadeGoverno.Nome;
        }
        else if (usuarioLogado is Prefeito)
        {
            ViewBag.Cargo = 'P';
        }
        else if (usuarioLogado is Secretario)
        {
            ViewBag.Cargo = 'S';
        }
        else
        {
            ViewBag.Cargo = 'U';
        }
        // TODO: outros cargos

        return View(requisicao);
    }

    [Route("Requisicao/Aprovar/{requisicaoid}")]
    public IActionResult Aprovar(int? requisicaoid)
    {
        if (requisicaoid == null)
        {
            return NotFound();
        }

        var requisicao = repositorio
        .RetornarTabela()
        .Include(r => r.Instituicao)
        .FirstOrDefault(r => r.RequisicaoID == requisicaoid);
        
        if (requisicao == null)
        {
            return NotFound();
        }

        if (requisicao.Status != StatusRequisicao.analise)
        {
            return BadRequest();
        }

        int usuarioID = Int32.Parse(User.FindFirst("ID").Value);
        var usuarioLogado = repositorio_usuario.RetornarPorId(usuarioID);

        if (usuarioLogado is Governador)
        {
            var prefeito_requisicao = repositorio_prefeito
            .RetornarTabela()
            .Include(p => p.CidadeGoverno)
            .FirstOrDefault(p => p.UsuarioID == requisicao.UsuarioID);

            if (requisicao.Instituicao.Verba >= requisicao.Verba)
            {
                prefeito_requisicao.CidadeGoverno.Verba += (double) requisicao.Verba;
                requisicao.Instituicao.Verba -= (double) requisicao.Verba;
                requisicao.Status = StatusRequisicao.aprovada;
                repositorio.Salvar();
            }
            else
            {
                return BadRequest(); // TODO: Erro personalizado
            }

            return RedirectToAction("Index", "Governador");
        }
        else if (usuarioLogado is Prefeito)
        {
            ViewBag.Cargo = 'P';
        }
        else if (usuarioLogado is Secretario)
        {
            ViewBag.Cargo = 'S';
        }
        else
        {
            ViewBag.Cargo = 'U';
        }
        // TODO: outros cargos, outros tipos requisicoes

        return NotFound();
    }

    [Route("Requisicao/Reprovar/{requisicaoid}")]
    public IActionResult Reprovar(int? requisicaoid)
    {
        if (requisicaoid == null)
        {
            return NotFound();
        }

        var requisicao = repositorio
        .RetornarTabela()
        .Include(r => r.Instituicao)
        .FirstOrDefault(r => r.RequisicaoID == requisicaoid);
        
        if (requisicao == null)
        {
            return NotFound();
        }

        if (requisicao.Status != StatusRequisicao.analise)
        {
            return BadRequest();
        }

        int usuarioID = Int32.Parse(User.FindFirst("ID").Value);
        var usuarioLogado = repositorio_usuario.RetornarPorId(usuarioID);

        requisicao.Status = StatusRequisicao.reprovada;
        repositorio.Salvar();

        return RedirectToAction("Index", "Governador");
    }

    public IActionResult Criar()
    {
        /* Pegar o id do usuario logado */
        int usuarioID = Int32.Parse(User.FindFirst("ID").Value);
        /* Pesquisar no repositorio este usuario pelo ID */
        var usuarioLogado = repositorio_usuario.RetornarPorId(usuarioID);

        if (usuarioLogado is Prefeito)
        {
            ViewBag.Cargo = 'P';
        }
        // TODO: Outros usuarios

        return View();
    }

    [HttpPost]
    public IActionResult Criar([Bind("Tipo, Descricao, Verba")] Requisicao requisicao)
    {
        if (requisicao.Tipo == TipoRequisicao.verba 
        && requisicao.Verba == null)
        {
            ModelState.AddModelError("Verba", "Para o tipo requisição uma verba é necessária");
        }

        if (ModelState.IsValid)
        {
             /* Pegar o id do usuario logado */
            int usuarioID = Int32.Parse(User.FindFirst("ID").Value);
            /* Pesquisar no repositorio este usuario pelo ID */
            var usuarioLogado = repositorio_usuario.RetornarPorId(usuarioID);

            if (usuarioLogado is Prefeito)
            {
                requisicao.Usuario = usuarioLogado;
                requisicao.UsuarioID = usuarioLogado.UsuarioID;

                var prefeitoLogado = repositorio_prefeito
                .RetornarTabela()
                .Include(p => p.CidadeGoverno)
                .FirstOrDefault(p => p.UsuarioID == usuarioLogado.UsuarioID);

                requisicao.Instituicao = prefeitoLogado.CidadeGoverno.Estado;
                requisicao.InstituicaoID = prefeitoLogado.CidadeGoverno.EstadoID;

                repositorio.Cadastrar(requisicao);
                repositorio.Salvar();
                return RedirectToAction("Index", "Prefeito");
            }
            // TODO: Outros usuarios
            return NotFound();
        }

        return RedirectToAction("Criar", "Requisicao");
    }
}