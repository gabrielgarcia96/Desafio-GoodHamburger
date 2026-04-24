using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Interfaces
{
    public interface IItemPedidoRepository
    {
        Task<ItemPedido?> ObterPorIdAsync(Guid id);
        Task<List<ItemPedido>> ListarAsync();
        Task AdicionarAsync(ItemPedido itemPedido);
        Task AtualizarAsync(ItemPedido itemPedido);
        Task RemoverAsync(ItemPedido itemPedido);
    }
}