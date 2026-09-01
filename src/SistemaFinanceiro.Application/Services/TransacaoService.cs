using ProjetoFinanceiro.Application.DTOs.Auth;
using ProjetoFinanceiro.Application.Interfaces;
using ProjetoFinanceiro.Domain.Entities;
using ProjetoFinanceiro.Domain.Interfaces;

namespace ProjetoFinanceiro.Application.Services;

public class TransacaoService : ITransacaoService
{
    private readonly ITransacaoRepository _transacaoRepository;

    public TransacaoService(ITransacaoRepository transacaoRepository)
    {
        _transacaoRepository = transacaoRepository;
    }

    public async Task<IEnumerable<TransacaoResponseDto>> ObterPorUsuarioAsync(Guid usuarioId)
    {
        var transacoes = await _transacaoRepository.ObterPorUsuarioIdAsync(usuarioId);

        return transacoes.Select(t => new TransacaoResponseDto(
                t.Id,
                t.Valor,
                t.Data,
                t.Tipo,
                t.Metodo,
                t.Categoria,
                t.UsuarioId
            ));
    }

    public async Task<TransacaoResponseDto> CriarAsync(Guid usuarioId, CriarTransacaoDto dto)
    {
        var transacao = new Transacao
        {
            Id = Guid.NewGuid(),
            UsuarioId = usuarioId,
            Valor = dto.Valor,
            Data = DateTime.UtcNow,
            Tipo = dto.Tipo,
            Metodo = dto.Metodo,
            Categoria = dto.Categoria
        };

        await _transacaoRepository.AdicionarAsync(transacao);

        return new TransacaoResponseDto
        (
            transacao.Id,
            transacao.Valor,
            transacao.Data,
            transacao.Tipo,
            transacao.Metodo,
            transacao.Categoria,
            transacao.UsuarioId
        );
    }
}