using GoodHamburguer.Domain.Entities;
using GoodHamburguer.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburguer.Infrastructure.Db;

public class AppContextDb : DbContext
{

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<ItemPedido> ItensPedido { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public AppContextDb(DbContextOptions<AppContextDb> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Produto>().HasData(
            new Produto { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Nome = "X Burger", Preco = 5.00m, Tipo = TipoProduto.Hamburguer },
            new Produto { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Nome = "X Egg", Preco = 4.50m, Tipo = TipoProduto.Hamburguer },
            new Produto { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Nome = "X Bacon", Preco = 7.00m, Tipo = TipoProduto.Hamburguer },
            new Produto { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Nome = "Batata frita", Preco = 2.00m, Tipo = TipoProduto.BatataFrita },
            new Produto { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Nome = "Refrigerante", Preco = 2.50m, Tipo = TipoProduto.Refrigerante }
        );
    }
}
