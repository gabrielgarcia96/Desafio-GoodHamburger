namespace GoodHamburguer.Api.Dtos
{
    public class AdicionarItemRequest
    {
        public Guid IdProduto { get; set; }
        public int Quantidade { get; set; }
    }
}
