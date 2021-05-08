using System.Threading.Tasks;
using TestePessoaWeb.Domain;

namespace TestePessoaWeb.Repository
{
    public interface ITestePessoaRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;

        Task<bool> SaveChangesAsync();

        Task<Pessoa[]> GetAllPessoasAsync();
        Task<Pessoa> GetPessoaAsyncById(int id);
        Task<Pessoa[]> GetPessoasAsyncByName(string name);

    }
}
