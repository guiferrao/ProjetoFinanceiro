using ProjetoFinanceiro.Domain.Enums;

namespace ProjetoFinanceiro.Application.DTOs.Auth;

public record TransacaoResponseDto(
    Guid Id,
    decimal Valor,
    DateTime Data,
    TipoTransacao Tipo,
    Metodo Metodo,
    string Categoria,
    Guid UsuarioId
);