namespace ProjetoFinanceiro.Application.DTOs.Auth;

public record TokenResponseDto(string Token, DateTime Expiracao);