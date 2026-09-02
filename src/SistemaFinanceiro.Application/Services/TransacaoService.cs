using ProjetoFinanceiro.Application.DTOs.Auth;
using ProjetoFinanceiro.Application.Interfaces;
using ProjetoFinanceiro.Domain.Entities;
using ProjetoFinanceiro.Domain.Interfaces;
using SistemaFinanceiro.Application.DTOs.Transacoes;

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

    public async Task<TransacaoResponseDto> AtualizarAsync(Guid id, Guid usuarioId, AtualizarTransacaoDto dto)
    {
        var transacao = await _transacaoRepository.ObterPorIdAsync(id);

        if(transacao == null || transacao.UsuarioId != usuarioId)
        {
            throw new Exception("Transacao nao encontrada ou acesso negado");
        }

        transacao.Valor = dto.Valor;
        transacao.Data = dto.Data;
        transacao.Tipo = dto.Tipo;
        transacao.Metodo = dto.Metodo;
        transacao.Categoria = dto.Categoria;

        await _transacaoRepository.AtualizarAsync(transacao);

        return new TransacaoResponseDto(
            transacao.Id,
            transacao.Valor,
            transacao.Data,
            transacao.Tipo,
            transacao.Metodo,
            transacao.Categoria,
            transacao.UsuarioId
        );
    }

    public async Task<bool> DeletarAsync(Guid id, Guid usuarioId)
    {
        var transacao = await _transacaoRepository.ObterPorIdAsync(id);

        if (transacao == null || transacao.UsuarioId != usuarioId)
        {
            return false;
        }

        await _transacaoRepository.DeletarAsync(transacao.Id); 
        return true;
    }
}