using BuscaPaginada.Models;
using Microsoft.EntityFrameworkCore;

namespace BuscaPaginada.Context;

public class TesteContext : DbContext
{
    public TesteContext(DbContextOptions options) : base(options)
    {
    }
    public virtual DbSet<Product> Products { get; set; }
}