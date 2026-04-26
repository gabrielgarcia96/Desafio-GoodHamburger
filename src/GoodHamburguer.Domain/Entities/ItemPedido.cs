namespace GoodHamburguer.Domain.Entities
{
    public class ItemPedido
    {
        public Guid Id { get; set; }
        public Guid IdPedido { get; set; }
        public Pedido? Pedido { get; set; }
        public Guid IdProduto { get; set; }
        public Produto? Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal Subtotal => Produto != null ? Produto.Preco * Quantidade : 0;
    }

}