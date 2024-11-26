using MinhaApiComSQLite.Enums;
using MinhaApiComSQLite.Models;

namespace MinhaApiComSQLite.Repositories.Interfaces
{
    public interface IProdutoRepository
    {

        Task<Produto> AddProduto(Produto produto);
        Task<List<Produto>> GetAllProdutos();
        Task<List<Produto>> GetProdutosWithFilterAndSorting(string nome, TipoOrdenacao tipoOrdenacao);
        Task<Produto> UpdateProduto(int id, Produto produto);
        Task<bool> DeleteProduto(int id);

    }
}
