namespace GoodHamburger.Domain.Entities
{
    public class ItemPedido
    {
        public Guid Id { get; set; }
        public Guid IdPedido { get; set; }
        public Pedido? Pedido { get; set; } 
        public Guid IdProduto { get; set; }
        public Produto Produto { get; set; } = new Produto(); 
        public int Quantidade { get; set; }
        public decimal Subtotal => Produto.Preco * Quantidade;
    }
}