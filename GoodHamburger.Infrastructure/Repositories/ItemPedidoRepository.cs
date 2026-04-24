using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.Repositories
{
    public class ItemPedidoRepository : IItemPedidoRepository
    {
        private readonly AppContextDb _context;

        public ItemPedidoRepository(AppContextDb context)
        {
            _context = context;
        }

        public async Task<ItemPedido?> ObterPorIdAsync(Guid id)
        {
            return await _context.ItensPedido
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<ItemPedido>> ListarAsync()
        {
            return await _context.ItensPedido
                .Include(i => i.Produto)
                .ToListAsync();
        }

        public async Task AdicionarAsync(ItemPedido itemPedido)
        {
            await _context.ItensPedido.AddAsync(itemPedido);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(ItemPedido itemPedido)
        {
            _context.ItensPedido.Update(itemPedido);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(ItemPedido itemPedido)
        {
            _context.ItensPedido.Remove(itemPedido);
            await _context.SaveChangesAsync();
        }
    }
}