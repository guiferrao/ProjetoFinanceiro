using System.Diagnostics.Contracts;

namespace ProjetoFinanceiro.Domain.Entities;

public class Transacao
{
    public Guid Id { get; set; }
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
    public enum TipoTransacao
    {
        Entrada = 1,
        Saida = 2
    }
    public TipoTransacao Tipo { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
}