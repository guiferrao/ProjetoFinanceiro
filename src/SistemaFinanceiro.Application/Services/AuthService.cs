using Microsoft.AspNetCore.Identity;
using ProjetoFinanceiro.Application.DTOs.Auth;
using ProjetoFinanceiro.Application.Interfaces;
using ProjetoFinanceiro.Domain.Entities;
using ProjetoFinanceiro.Domain.Interfaces;

namespace ProjetoFinanceiro.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITokenService _tokenService;
    private readonly PasswordHasher<Usuario> _passwordHasher;

    public AuthService(IUsuarioRepository usuarioRepository, ITokenService tokenService)
    {
        _usuarioRepository = usuarioRepository;
        _tokenService = tokenService;
        _passwordHasher = new PasswordHasher<Usuario>();
    }

    public async Task<TokenResponseDto> RegistrarAsync(RegistrarUsuarioDto dto)
    {
        var emailExiste = await _usuarioRepository.EmailExisteAsync(dto.Email);
        if (emailExiste)
        {
            throw new Exception("Este e-mail já está em uso");
        }

        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = dto.Nome,
            Email = dto.Email
        };

        usuario.SenhaHash = _passwordHasher.HashPassword(usuario, dto.Senha);

        await _usuarioRepository.AdicionarAsync(usuario);

        var token = _tokenService.GerarToken(usuario);
        return new TokenResponseDto(token, DateTime.UtcNow.AddHours(2));
    }

    public async Task<TokenResponseDto> LoginAsync(LoginDto dto)
    {
        var usuario = await _usuarioRepository.ObterPorEmailAsync(dto.Email);

        if(usuario == null)
        {
            throw new Exception("Email ou senha invalidos");
        }

        var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.SenhaHash, dto.Senha);

        if (resultado == PasswordVerificationResult.Failed)
        {
            throw new Exception("Email ou senha invalidos");
        }

        var token = _tokenService.GerarToken(usuario);
        return new TokenResponseDto(token, DateTime.UtcNow.AddHours(2));
    }
}