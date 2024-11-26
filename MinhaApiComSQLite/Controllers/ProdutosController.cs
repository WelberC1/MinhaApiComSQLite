using Microsoft.AspNetCore.Mvc;
using MinhaApiComSQLite.Data;
using MinhaApiComSQLite.Models;
using Microsoft.EntityFrameworkCore;
using MinhaApiComSQLite.Repositories;

namespace MinhaApiComSQLite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutoRepository produtoRepository;

        public ProdutosController(ProdutoRepository produtoRepository)
        {
            this.produtoRepository = produtoRepository;
        }

        // GET: api/Produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            return await produtoRepository.GetAllProdutos();
        }

        // POST: api/Produtos -> obj. produto obtido no corpo da requisição
        [HttpPost]
        public async Task<ActionResult<Produto>> AddProduto([FromBody] Produto produto)
        {
            return await produtoRepository.AddProduto(produto);
        }


        // PUT: api/Produtos/id
        [HttpPut("{id}")]
        public async Task<ActionResult<Produto>> UpdateProduto([FromRoute] int id, [FromBody] Produto produto)
        {
            return await produtoRepository.UpdateProduto(id, produto);
        }

        // DELETE: api/Produtos/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteProduto(int id)
        {
            return await produtoRepository.DeleteProduto(id);
        }
    }
}
