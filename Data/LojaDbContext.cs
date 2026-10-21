using LojaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaApi.Data;

public class LojaDbContext : DbContext
{
    public LojaDbContext(DbContextOptions<LojaDbContext> options) : base(options) { }

    public DbSet<Produto> Produtos => Set<Produto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
}