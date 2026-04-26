namespace GoodHamburguer.Api.Dtos;

public class PedidoResponse
{
    public Guid Id { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Desconto { get; set; }
    public decimal Total { get; set; }
    public List<ItemPedidoResponse> Itens { get; set; } = new();
}
