using Microsoft.EntityFrameworkCore;
using ProjetoFinanceiro.Domain.Entities;
using ProjetoFinanceiro.Domain.Interfaces;
using ProjetoFinanceiro.Infrastructure.Data;

namespace ProjetoFinanceiro.Infrastructure.Repositories;

public class TransacaoRepository : ITransacaoRepository
{
    private readonly AppDbContext _context;

    public TransacaoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Transacao transacao)
    {
        await _context.Transacoes.AddAsync(transacao);
        await _context.SaveChangesAsync();
    }

    public async Task<Transacao?> ObterPorIdAsync(Guid id)
    {
        return await _context.Transacoes
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Transacao>> ObterPorUsuarioIdAsync(Guid usuarioId)
    {
        return await _context.Transacoes
            .Where(t => t.UsuarioId == usuarioId)
            .ToListAsync();
    }

    public async Task AtualizarAsync(Transacao transacao)
    {
        _context.Transacoes.Update(transacao);
        await _context.SaveChangesAsync();
    }

    public async Task DeletarAsync(Guid id)
    {
        var transacao = await ObterPorIdAsync(id);
        if(transacao != null)
        {
            _context.Transacoes.Remove(transacao);
            await _context.SaveChangesAsync();
        }
    }
}