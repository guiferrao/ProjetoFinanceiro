using ProjetoFinanceiro.Domain.Entities;

namespace ProjetoFinanceiro.Application.Interfaces;

public interface ITokenService
{
    string GerarToken(Usuario usuario);
}