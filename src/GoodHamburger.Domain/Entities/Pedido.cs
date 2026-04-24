using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Domain.Entities;

public class Pedido
{
    public Guid Id { get; set; }

    private List<ItemPedido> _itens = new();
    public IReadOnlyCollection<ItemPedido> Itens => _itens;
    public decimal Desconto { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Total { get; set; }

    public void DescontoCombo()
    {
        SubTotal = _itens.Sum(x => x.Produto.Preco);

        bool existeHamburguer = _itens.Any(x => x.Produto.Tipo == TipoProduto.Hamburguer);
        bool existeBatata = _itens.Any(x => x.Produto.Tipo == TipoProduto.BatataFrita);
        bool existeRefrigerante = _itens.Any(x => x.Produto.Tipo == TipoProduto.Refrigerante);

        if (existeHamburguer && existeBatata && existeRefrigerante)
        {
            Desconto = SubTotal * 0.20m;
        }
        else if (existeHamburguer && existeRefrigerante)
        {
            Desconto = SubTotal * 0.15m;
        }
        else if (existeHamburguer && existeBatata)
        {
            Desconto = SubTotal * 0.10m;
        }
        else
        {
            Desconto = 0;
        }

        Total = SubTotal - Desconto;
    }

    public void AdicionarItem(ItemPedido newProduto)
    {
        if (_itens.Any(i => i.Produto.Tipo == newProduto.Produto.Tipo))
        {
            throw new InvalidOperationException($"O produto do tipo {newProduto.Produto.Tipo} já foi adicionado ao pedido.");
        };

        _itens.Add(newProduto);
    }
    
}
