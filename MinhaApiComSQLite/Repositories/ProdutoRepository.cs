﻿using MinhaApiComSQLite.Data;
using MinhaApiComSQLite.Enums;
using MinhaApiComSQLite.Models;
using MinhaApiComSQLite.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore; // Use o namespace correto


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
            //primeira validação: O nome não pode estar vazio ou em branco.
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

        public async Task<bool> DeleteProduto(int ID)
        {
            var produto = await _context.Produtos.FindAsync(ID);

            if (produto == null)
                return false; 

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Produto>> GetAllProdutos()
        {
            //busca todos produtos e coloca numa lista
            var produtos= await _context.Produtos.ToListAsync();

            //passa por todos produtos da lista e verifica se estão em promoção
            foreach (var produto in produtos)
            {
                if (produto.Nome.Contains("PROMOÇÃO", StringComparison.OrdinalIgnoreCase))
                    produto.Nome = $"{produto.Nome} [Em Promoção]";
            }
            return produtos.OrderBy(p => p.Preco).ToList();
        }

        public Task<List<Produto>> GetProdutosWithFilterAndSorting(string nome, TipoOrdenacao tipoOrdenacao)
        {
            //esse aqui é um pouco mais complexo...
            throw new NotImplementedException();
        }

        //atualiza produtos
        public async Task<Produto> UpdateProduto(int id, Produto produtoAtualizado)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                throw new Exception("Produto não encontrado.");
            }

            if (produtoAtualizado.Preco < 0)
            {
                throw new Exception("O preço não pode ser menor que zero.");
            }

            if (!string.IsNullOrEmpty(produtoAtualizado.Nome))
            {
                produtoAtualizado.Nome = LetraMaiusculaPrimeiraLetra(produtoAtualizado.Nome);
            }

            produto.Nome = produtoAtualizado.Nome;
            produto.Preco = produtoAtualizado.Preco;

            await _context.SaveChangesAsync();

            return produto;
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