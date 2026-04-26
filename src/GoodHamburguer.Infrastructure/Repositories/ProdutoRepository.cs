using GoodHamburguer.Domain.Entities;
using GoodHamburguer.Infrastructure.Db;
using GoodHamburguer.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburguer.Infrastructure.Repositories;

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
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductById(Guid id)
    {
        var produto = await _context.Produtos.FindAsync(id);

        if (produto == null)
            return;
        
        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

    }

    public Task<List<Produto>> GetAllAsync()
    {
        return _context.Produtos.ToListAsync();
    }

    public async Task<Produto?> GetProductById(Guid id)
    {
        return await _context.Produtos
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task UpdateProduct(Produto produto)
    {
        _context.Produtos.Update(produto);
        await _context.SaveChangesAsync();
    }

}
