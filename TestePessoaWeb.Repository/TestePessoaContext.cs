using Microsoft.EntityFrameworkCore;
using TestePessoaWeb.Domain;

namespace TestePessoaWeb.Repository
{
    public class TestePessoaContext : DbContext
    {
        public TestePessoaContext(DbContextOptions<TestePessoaContext> options) : base(options)
        { }

        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
