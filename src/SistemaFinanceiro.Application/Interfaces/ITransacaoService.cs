using ProjetoFinanceiro.Application.DTOs.Auth;

namespace ProjetoFinanceiro.Application.Interfaces;

public interface ITransacaoService
{
    Task<IEnumerable<TransacaoResponseDto>> ObterPorUsuarioAsync(Guid usuarioId);
    Task<TransacaoResponseDto> CriarAsync(Guid usuarioId, CriarTransacaoDto dto);
}