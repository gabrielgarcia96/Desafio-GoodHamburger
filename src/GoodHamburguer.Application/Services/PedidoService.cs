using GoodHamburguer.Application.Interfaces;
using GoodHamburguer.Domain.Entities;

namespace GoodHamburguer.Application.Services;

public class PedidoService
{
    private readonly IPedidoRepository _pedidoRepository;

    public PedidoService(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task CriarPedido(Pedido pedido)
    {
        if (!pedido.Itens.Any())
        {
            throw new InvalidOperationException("O pedido nao pode ser criado sem itens");
        }

        await _pedidoRepository.AdicionarAsync(pedido);
    }

    public async Task AtualizarPedido(Pedido pedido)
    {
        pedido.CalcularTotais();

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
