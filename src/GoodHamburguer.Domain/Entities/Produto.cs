using GoodHamburguer.Domain.Enums;

namespace GoodHamburguer.Domain.Entities;

public class Produto
{
  public Guid Id { get; set; }
   public string Nome { get; set; } = string.Empty;
   public decimal Preco { get; set; } = 0;
   public TipoProduto Tipo { get; set; }
}
