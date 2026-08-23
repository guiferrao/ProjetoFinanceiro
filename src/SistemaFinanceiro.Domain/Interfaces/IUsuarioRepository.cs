using ProjetoFinanceiro.Domain.Entities;

namespace ProjetoFinanceiro.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario> AdicionarAsync(Usuario usuario);
    Task<Usuario?> ObterPorEmailAsync(string email);
    Task<bool> EmailExisteAsync(string email);
}