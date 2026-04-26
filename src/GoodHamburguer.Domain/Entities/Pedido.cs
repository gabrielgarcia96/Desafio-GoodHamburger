using GoodHamburguer.Domain.Enums;

namespace GoodHamburguer.Domain.Entities;

public class Pedido
{
    public Guid Id { get; set; }

    public List<ItemPedido> Itens { get; set; } = new();
    public decimal Desconto { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Total { get; set; }

 
    public void AdicionarItem(ItemPedido newProduto)
    {
        if (Itens.Any(i => i.Produto.Tipo == newProduto.Produto.Tipo))
        {
            throw new InvalidOperationException($"O produto do tipo {newProduto.Produto.Tipo} já foi adicionado ao pedido.");
        };

        Itens.Add(newProduto);

        CalcularTotais();
    }


    public void CalcularTotais()
    {
        if (!Itens.Any())
        {
            throw new InvalidOperationException($"O pedido deve ter pelo menos um item.");
        }

        SubTotal = Itens.Sum(x => x.Subtotal);

        bool existeHamburguer = Itens.Any(x => x.Produto.Tipo == TipoProduto.Hamburguer);
        bool existeBatata = Itens.Any(x => x.Produto.Tipo == TipoProduto.BatataFrita);
        bool existeRefrigerante = Itens.Any(x => x.Produto.Tipo == TipoProduto.Refrigerante);

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

}
