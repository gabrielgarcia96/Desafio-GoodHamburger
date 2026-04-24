using GoodHamburger.Domain.Entities;
using GoodHamburger.Infrastructure.Db;
using GoodHamburguer.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.Repositories;

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
    }

    public async Task AtualizarAsync(Pedido pedido)
    {

           await _context.Pedidos.Where(p => p.Id == pedido.Id).ExecuteUpdateAsync(p => p
                .SetProperty(p => p.Desconto, pedido.Desconto)
                .SetProperty(p => p.SubTotal, pedido.SubTotal)
                .SetProperty(p => p.Total, pedido.Total));
    }

    public async Task<List<Pedido>> ListarAsync()
    {
        return await _context.Pedidos.ToListAsync();
    }

    public async Task<Pedido?> ObterPorIdAsync(Guid id)
    {
        return await _context.Pedidos.Where(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
       await _context.Pedidos.Where(p => p.Id == id).ExecuteDeleteAsync();
    }
}
