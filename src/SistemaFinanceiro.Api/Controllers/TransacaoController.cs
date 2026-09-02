using System.Drawing;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using ProjetoFinanceiro.Application.DTOs.Auth;
using ProjetoFinanceiro.Application.Interfaces;
using ProjetoFinanceiro.Domain.Interfaces;
using SistemaFinanceiro.Application.DTOs.Transacoes;

namespace ProjetoFinanceiro.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/transacoes")]
public class TransacoesController : ControllerBase
{
    private readonly ITransacaoService _transacaoService;

    public TransacoesController(ITransacaoService transacaoService)
    {
        _transacaoService = transacaoService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTransacoes()
    {
        var usuarioId = ObterUsuarioIdLogado();
        var transacoes = await _transacaoService.ObterPorUsuarioAsync(usuarioId);
        return Ok(transacoes);
    }

    [HttpPost]
    public async Task<IActionResult> CriarTransacao([FromBody] CriarTransacaoDto dto)
    {
        var usuarioId = ObterUsuarioIdLogado();
        var novaTransacao = await _transacaoService.CriarAsync(usuarioId, dto);
        return Ok(novaTransacao);
    }

    private Guid ObterUsuarioIdLogado()
    {
        var claimId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(claimId, out var id) ? id : throw new Exception("Usuario nao identificado no token");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarTransacaoDto dto)
    {
        try
        {
            var usuarioId = ObterUsuarioIdLogado();
            var transacaoAtualizada = await _transacaoService.AtualizarAsync(id, usuarioId, dto);
            return Ok(transacaoAtualizada);
        }
        catch (Exception ex)
        {
            return BadRequest(new { erro = ex.Message} );
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(Guid id)
    {
        var usuarioId = ObterUsuarioIdLogado();
        var sucesso = await _transacaoService.DeletarAsync(id, usuarioId);

        if (!sucesso)
        {
            return NotFound(new { erro = "transacao nao encontrada"});
        }

        return NoContent();
    }
}