using ProjetoFinanceiro.Domain.Entities;

namespace ProjetoFinanceiro.Domain.Interfaces;

public interface ITransacaoRepository
{
    Task<Transacao> AdicionarAsync(Transacao transacao);
    Task<Transacao?> ObterPorIdAsync(Guid Id);
    Task<IEnumerable<Transacao>> ObterPorUsuarioIdAsync(Guid UsuarioId);
    Task AtualizarAsync(Transacao transacao);
    Task DeletarAsync(Transacao transacao);
}
