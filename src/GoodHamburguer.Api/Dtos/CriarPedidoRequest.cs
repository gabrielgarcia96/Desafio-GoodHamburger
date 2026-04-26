using GoodHamburguer.Domain.Entities;

namespace GoodHamburguer.Api.Dtos
{
    public class CriarPedidoRequest
    {
        public List<ItemPedidoRequest> Itens { get; set; } = new();
    }
}
