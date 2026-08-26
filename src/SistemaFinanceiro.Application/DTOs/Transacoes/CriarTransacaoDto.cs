using static ProjetoFinanceiro.Domain.Entities.Transacao;

namespace ProjetoFinanceiro.Application.DTOs.Auth;

public record CriarTransacaoDto(
    decimal Valor,
    DateTime Data,
    TipoTransacao Tipo,
    Guid UsuarioId
);