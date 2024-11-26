using MinhaApiComSQLite.Data;
using MinhaApiComSQLite.Enums;
using MinhaApiComSQLite.Models;
using MinhaApiComSQLite.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MinhaApiComSQLite.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext _context)
        {
            this._context = _context;
        }

        public async Task<Produto> AddProduto(Produto produto)
        {
            //primeira validação: o nome não pode estar vazio ou em branco.
            if (string.IsNullOrWhiteSpace(produto.Nome))
                throw new ArgumentException("O nome do produto não pode ser vazio ou nulo.");

            if (produto.Preco <= 0)
                throw new ArgumentException("O preço do produto deve ser maior que zero.");

            //pega o nome digitado e transforma em letra maiuscula antes de salvar no BD: exemplo: pão -> PÃO
            produto.Nome = produto.Nome.ToUpper();

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<List<Produto>> GetAllProdutos()
        {
            //busca todos produtos e coloca numa lista
            var produtos = await _context.Produtos.ToListAsync();

            //passa por todos produtos da lista e verifica se estão em promoção
            foreach (var produto in produtos)
            {
                if (produto.Nome.Contains("PROMOÇÃO", StringComparison.OrdinalIgnoreCase))
                    produto.Nome = $"{produto.Nome} [Em Promoção]";
            }

            return produtos.OrderBy(p => p.Preco).ToList();
        }

        public async Task<List<Produto>> GetProdutosWithFilterAndSorting(string nome, TipoOrdenacao tipoOrdenacao)
        {
            var query = _context.Produtos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nome))
            {
                //peguei o nome que o usuario digitou e passei pra maiusculo, assim não será case sensitive
                query = query.Where(p => p.Nome.ToUpper().Contains(nome.ToUpper()));
            }

            switch (tipoOrdenacao)
            {
                //o sqlite nao ordena suporta ordenacao de numeros do tipo decimal
                //então fiz uma conversão direta pra double, assim ele consegue filtrar
                case TipoOrdenacao.ASC:
                    query = query.OrderBy(p => (double) p.Preco);
                    break;
                case TipoOrdenacao.DES:
                    query = query.OrderByDescending(p => (double) p.Preco);
                    break;
                default:
                    query = query.OrderBy(p => (double) p.Preco);
                    break;
            }

            return await query.ToListAsync();
        }

        //atualiza produtos
        public async Task<Produto> UpdateProduto(int id, Produto produtoAtualizado)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return null;
            }

            //pensei em fazer as validacoes em um if só utilizando o ||, mas prefiro o modelo individual de mensagem de erro
            //fica mais facil pra identificar um eventual problema.
            if (produtoAtualizado.Preco <= 0)
            {
                throw new ArgumentException("O preço deve ser maior que zero.");
            }
            
            if (string.IsNullOrWhiteSpace(produtoAtualizado.Nome))
            {
                throw new ArgumentException("O nome do produto não pode ser vazio ou nulo.");
            }

            //coloca a primeira letra em maiusculo 
            produto.Nome = LetraMaiusculaPrimeiraLetra(produtoAtualizado.Nome);
            produto.Preco = produtoAtualizado.Preco;

            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task<bool> DeleteProduto(int ID)
        {
            var produto = await _context.Produtos.FindAsync(ID);

            if (produto == null)
                return false;

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return true;
        }

        //o nome do método é ruim, mas basicamente ele vai separar o nome do produto em dois utilizando o split (defini o espaço pra ser o separador).
        //vai pegar a letra na posicao 0 da palavra e colocar maiusculo, e o resto fica minusculo
        private string LetraMaiusculaPrimeiraLetra(string str)
        {
            return string.Join(" ", str.Split(' ')
                                       .Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower()));
        }
    }
}
