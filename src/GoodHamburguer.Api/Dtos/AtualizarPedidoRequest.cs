namespace GoodHamburguer.Api.Dtos
{
    public class AtualizarPedidoRequest
    {
        public Guid IdPedido { get; set; }
        public List<ItemPedidoRequest>? Itens { get; set; }
    }
}
