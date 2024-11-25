using Microsoft.EntityFrameworkCore;
using MinhaApiComSQLite.Models;

namespace MinhaApiComSQLite.Data
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
    }

}
