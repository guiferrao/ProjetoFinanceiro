using ProjetoFinanceiro.Application.DTOs.Auth;

namespace ProjetoFinanceiro.Application.Interfaces;

public interface IAuthService
{
    Task<TokenResponseDto> RegistrarAsync(RegistrarUsuarioDto registroDto);
    Task<TokenResponseDto> LoginAsync(LoginDto loginDto);
}