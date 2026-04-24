using GoodHamburger.Domain.Entities;

namespace GoodHamburguer.Application.Interfaces;

public interface IProdutoRepository
{
    Task<List<Produto>> GetAllAsync();
    Task CreatProduct(Produto produto);
    Task GetProductById(Guid id);
    Task UpdateProduct(Produto produto);
    Task DeleteProductById(Guid id);
}
