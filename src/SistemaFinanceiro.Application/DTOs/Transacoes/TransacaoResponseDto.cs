using ProjetoFinanceiro.Domain.Enums;

namespace ProjetoFinanceiro.Application.DTOs.Auth;

public record TransacaoResponseDto(
    Guid Id,
    decimal Valor,
    DateTime Data,
    TipoTransacao Tipo,
    Guid UsuarioId
);