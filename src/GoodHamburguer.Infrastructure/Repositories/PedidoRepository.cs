using GoodHamburguer.Domain.Entities;
using GoodHamburguer.Infrastructure.Db;
using GoodHamburguer.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburguer.Infrastructure.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly AppContextDb _context; 

        public PedidoRepository(AppContextDb context)
        {
            _context = context;
        }
    public async Task AdicionarAsync(Pedido pedido)
    {
       await _context.Pedidos.AddAsync(pedido);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Pedido pedido)
    {
        _context.Pedidos.Update(pedido);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Pedido>> ListarAsync()
    {
        return await _context.Pedidos
            .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
            .ToListAsync();
    }

    public async Task<Pedido?> ObterPorIdAsync(Guid id)
    {
        return await _context.Pedidos
            .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task DeleteAsync(Guid id)
    {
        var pedido = await _context.Pedidos.FindAsync(id);

        if (pedido == null)
            return;

        _context.Pedidos.Remove(pedido);
        await _context.SaveChangesAsync();
    }
}
