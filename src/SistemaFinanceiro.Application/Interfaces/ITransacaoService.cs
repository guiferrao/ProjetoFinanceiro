using ProjetoFinanceiro.Application.DTOs.Auth;
using SistemaFinanceiro.Application.DTOs.Transacoes;

namespace ProjetoFinanceiro.Application.Interfaces;

public interface ITransacaoService
{
    Task<IEnumerable<TransacaoResponseDto>> ObterPorUsuarioAsync(Guid usuarioId);
    Task<TransacaoResponseDto> CriarAsync(Guid usuarioId, CriarTransacaoDto dto);
    Task<TransacaoResponseDto> AtualizarAsync(Guid id, Guid usuarioId, AtualizarTransacaoDto dto);
    Task<bool> DeletarAsync(Guid id, Guid usuarioId);
}