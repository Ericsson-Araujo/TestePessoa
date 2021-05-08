using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TestePessoaWeb.Domain;

namespace TestePessoaWeb.Repository
{
    public class TestePessoaRepository : ITestePessoaRepository
    {
        private readonly TestePessoaContext _context;

        public TestePessoaRepository(TestePessoaContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Remove<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<Pessoa[]> GetAllPessoasAsync()
        {
            return await _context.Pessoas.OrderBy(p => p.Nome)
                .Take(1000).ToArrayAsync();
        }

        public async Task<Pessoa> GetPessoaAsyncById(int id)
        {
            return await _context.Pessoas.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pessoa[]> GetPessoasAsyncByName(string name)
        {
            return await _context.Pessoas
                .Where(p => p.Nome.ToLower().Contains(name.ToLower()))
                .OrderBy(p => p.Nome).Take(1000).ToArrayAsync();
        }

    }
}
