using GoodHamburger.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.Db;

public class AppContextDb : DbContext
{

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<ItemPedido> ItensPedido { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public AppContextDb(DbContextOptions<AppContextDb> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
    }
}
