using GoodHamburguer.Domain.Entities;

namespace GoodHamburguer.Application.Interfaces;

public interface IPedidoRepository
{
    Task<Pedido?> ObterPorIdAsync(Guid id);
    Task<List<Pedido>> ListarAsync();
    Task AdicionarAsync(Pedido pedido);
    Task AtualizarAsync(Pedido pedido);
    Task DeleteAsync(Guid id);
}
