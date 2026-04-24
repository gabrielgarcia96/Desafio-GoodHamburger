using GoodHamburger.Domain.Entities;
using GoodHamburger.Infrastructure.Db;
using GoodHamburguer.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppContextDb _context;

    public ProdutoRepository(AppContextDb context)
    {
        _context = context;
    }

    public async Task CreatProduct(Produto produto)
    {
       await _context.Produtos.AddAsync(produto);
    }

    public async Task DeleteProductById(Guid id)
    {
        await _context.Produtos.Where(p => p.Id == id).ExecuteDeleteAsync();
    }

    public Task<List<Produto>> GetAllAsync()
    {
        return _context.Produtos.ToListAsync();
    }

    public async Task GetProductById(Guid id)
    {
        await _context.Produtos.Where(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task UpdateProduct(Produto produto)
    {
        await _context.Produtos.Where(p => p.Id == produto.Id).ExecuteUpdateAsync(p => p
            .SetProperty(p => p.Nome, produto.Nome)
            .SetProperty(p => p.Preco, produto.Preco)
            .SetProperty(p => p.Tipo, produto.Tipo));
    }
}
