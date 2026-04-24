using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Entities;
using GoodHamburguer.Application.Interfaces;

namespace GoodHamburguer.Application.Services;

public class PedidoService
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IItemPedidoRepository _itemPedidoRepository;

    public PedidoService(IPedidoRepository pedidoRepository, IItemPedidoRepository itemPedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
        _itemPedidoRepository = itemPedidoRepository;
    }

    public async Task CriarPedido(Pedido pedido)
    {
        pedido.DescontoCombo();

        foreach(var itemPedido in pedido.Itens)
        {
            pedido.AdicionarItem(itemPedido);
            await _itemPedidoRepository.AdicionarAsync(itemPedido);
        }

        await _pedidoRepository.AdicionarAsync(pedido);
    }

    public async Task AtualizarPedido(Pedido pedido)
    {
        pedido.DescontoCombo();

        foreach(var itemPedido in pedido.Itens)
        {
            await _itemPedidoRepository.AtualizarAsync(itemPedido);
        }

        await _pedidoRepository.AtualizarAsync(pedido);
    }

    public async Task<Pedido?> ObterPedidoId(Guid id)
    {
      return  await _pedidoRepository.ObterPorIdAsync(id);
    }

    public async Task<List<Pedido>> ListarPedidos()
    {
       return await _pedidoRepository.ListarAsync();
    }
   

}
