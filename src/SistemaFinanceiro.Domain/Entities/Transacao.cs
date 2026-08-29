using ProjetoFinanceiro.Domain.Enums;

namespace ProjetoFinanceiro.Domain.Entities;

public class Transacao
{
    public Guid Id { get; set; }
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
    public TipoTransacao Tipo { get; set; }
    public Metodo Metodo { get; set; }
    public string Categoria { get; set; } = string.Empty;
    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
}