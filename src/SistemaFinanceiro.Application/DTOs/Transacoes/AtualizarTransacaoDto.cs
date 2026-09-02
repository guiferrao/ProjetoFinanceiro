namespace SistemaFinanceiro.Application.DTOs.Transacoes;

public record AtualizarTransacaoDto(
    decimal Valor,
    DateTime Data,
    ProjetoFinanceiro.Domain.Enums.TipoTransacao Tipo,
    ProjetoFinanceiro.Domain.Enums.Metodo Metodo,
    string Categoria
);