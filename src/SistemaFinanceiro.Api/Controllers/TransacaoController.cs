using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoFinanceiro.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/transacoes")]
public class TransacoesController : ControllerBase
{
    [HttpGet]
    public IActionResult GetTrasancoes()
    {
        var nomeUsuario = User.Identity?.Name;

        return Ok(new
        {
            mensagem = $"Acesso liberado! Bem vindo, {nomeUsuario}"
        });
    }
}